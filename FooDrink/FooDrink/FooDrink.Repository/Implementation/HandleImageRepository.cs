using FooDrink.Database;
using FooDrink.DTO.Request.Image;
using FooDrink.DTO.Response.Image;
using FooDrink.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FooDrink.Repository.Implementation
{
    public class HandleImageRepository : IHandleImageRepository
    {
        private readonly FooDrinkDbContext _context;

        public HandleImageRepository(FooDrinkDbContext context)
        {
            _context = context;

        }

        public async Task<UploadImageResponse> UploadImagesAsync(UploadImageRequest request, string webRootPath)
        {
            UploadImageResponse response = new();

            try
            {
                string folderPath = Path.Combine(webRootPath, "image", request.EntityType.ToLower(), request.EntityId.ToString());
                if (!Directory.Exists(folderPath))
                {
                    _ = Directory.CreateDirectory(folderPath);
                }

                foreach (IFormFile image in request.Images)
                {
                    if (image.Length > 0 && IsValidImage(image))
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                        string filePath = Path.Combine(folderPath, fileName);

                        using (FileStream stream = new(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }

                        switch (request.EntityType.ToLower())
                        {
                            case "restaurant":
                                if (_context.Restaurants == null)
                                {
                                    throw new NullReferenceException("Restaurant database context is null");
                                }
                                Database.Models.Restaurant? restaurant = await _context.Restaurants.FindAsync(request.EntityId);
                                if (restaurant != null)
                                {
                                    restaurant.ImageList += (string.IsNullOrEmpty(restaurant.ImageList) ? "" : ",") + $"/image/{request.EntityType.ToLower()}/{request.EntityId}/{fileName}";
                                    _ = _context.Restaurants.Update(restaurant);
                                }
                                break;
                            case "product":
                                if (_context.Products == null)
                                {
                                    throw new NullReferenceException("Product database context is null");
                                }
                                Database.Models.Product? product = await _context.Products.FindAsync(request.EntityId);
                                if (product != null)
                                {
                                    product.ImageList += (string.IsNullOrEmpty(product.ImageList) ? "" : ",") + $"/image/{request.EntityType.ToLower()}/{request.EntityId}/{fileName}";
                                    _ = _context.Products.Update(product);
                                }
                                break;
                            case "user":
                                if (_context.Users == null)
                                {
                                    throw new NullReferenceException("User database context is null");
                                }
                                Database.Models.User? user = await _context.Users.FindAsync(request.EntityId);
                                if (user != null)
                                {
                                    user.Image += (string.IsNullOrEmpty(user.Image) ? "" : ",") + $"/image/{request.EntityType.ToLower()}/{request.EntityId}/{fileName}";
                                    _ = _context.Users.Update(user);
                                }
                                break;
                            case "review":
                                if (_context.Reviews == null)
                                {
                                    throw new NullReferenceException("Restaurant database context is null");
                                }
                                Database.Models.Review? review = await _context.Reviews.FindAsync(request.EntityId);
                                if (review != null)
                                {
                                    review.ImageList += (string.IsNullOrEmpty(review.ImageList) ? "" : ",") + $"/image/{request.EntityType.ToLower()}/{request.EntityId}/{fileName}";
                                    _ = _context.Reviews.Update(review);
                                }
                                break;

                            default:
                                break;
                        }

                        _ = await _context.SaveChangesAsync();

                        response.ImageUrls.Add($"/image/{request.EntityType.ToLower()}/{request.EntityId}/{fileName}");
                    }
                    else
                    {
                        response.ErrorMessage = "Invalid image file.";
                        response.Success = false;
                        return response;
                    }
                }

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = $"An error occurred while uploading images: {ex.Message}";
            }

            return response;
        }

        /// <summary>
        /// GetImageUrl
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="entityType"></param>
        /// <param name="entityId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetImageUrl(string rootPath, string entityType, Guid entityId, string fileName)
        {
            return $"/image/{entityType.ToLower()}/{entityId}/{fileName}";
        }

        /// <summary>
        /// GetEntityImageListAsync
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="entityType"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        public Task<List<string>> GetEntityImageListAsync(string rootPath, string entityType, Guid entityId)
        {
            string folderPath = Path.Combine(rootPath, "image", entityType.ToLower(), entityId.ToString());
            if (!Directory.Exists(folderPath))
            {
                return Task.FromResult(new List<string>());
            }

            string[] imageFiles = Directory.GetFiles(folderPath);
            List<string> imageUrls = new();
            foreach (string imageFile in imageFiles)
            {
                string fileName = Path.GetFileName(imageFile);
                imageUrls.Add(GetImageUrl(rootPath, entityType, entityId, fileName));
            }

            return Task.FromResult(imageUrls);
        }


        private bool IsValidImage(IFormFile file)
        {
            string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return allowedExtensions.Contains(extension);
        }
    }

}

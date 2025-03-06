namespace Application.Utilities
{
    public class SavePhoto
    {
        public static async Task<string> SavePhotoAsync(string photoSection, string fileName, Stream fileStream)
        {
            var directoryPath = Path.Combine("uploads", photoSection);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var filePath = Path.Combine(directoryPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fileStream.CopyToAsync(stream);
            }

            //when deployed use the https url of the server to access the file from the client side 
            var fileUrl = photoSection + "/" + fileName;


            //event to upload the file to a cloud storage like AWS S3 or Azure Blob Storage

            //using rabbbitmq to publish the file url to a queue for a microservice to consume and upload to a cloud storage

            return fileUrl;


        }

    }
}
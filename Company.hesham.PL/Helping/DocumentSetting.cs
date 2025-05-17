namespace Company.hesham.PL.Helping
{
    public static class DocumentSetting
    {
        public static string Upload(IFormFile file,string folderName)
        {
            //1  Folder Path
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\", folderName);
            
            //2 file name
            var fileName= $"{Guid.NewGuid() }{file.FileName}";
            //3 File Path
            var  filePath = Path.Combine(FolderPath, fileName);

            using var FileStream = new FileStream(filePath,FileMode.Create);

            file.CopyTo(FileStream);
            return fileName;
       
        }
        public static void Delete(string FileName,string FolderName)
        {
            var FilePath = Path.Combine(
                                        Directory.GetCurrentDirectory(),
                                        @"wwwroot\Files\",
                                        FolderName,
                                        FileName
                                        );
            if(File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }
    }
}

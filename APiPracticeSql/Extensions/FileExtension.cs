using Microsoft.AspNetCore.Http;

namespace ApiPractice.DAL.Extensions
{
    public static class FileExtension
    {
        public static string Save(this IFormFile file,string root,string folder)
        {
            string newFileName=Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
            string path= Path.Combine(root,"wwwroot",folder,newFileName);
            using FileStream fs=new FileStream(path,FileMode.Create);
            file.CopyTo(fs);
            return newFileName;
        }
    }
}

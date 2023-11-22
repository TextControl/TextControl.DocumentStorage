using DocumentStorage.Models;
using LiteDB;

namespace DocumentStorage.Helpers
{
    public static class Storage
    {
        private static readonly LiteDatabase db = new LiteDatabase(@"Filename=App_Data/documents.db; Connection=shared");

        public static void AddTemplate(MemoryStream stream, string id = null, string name = "template.tx")
        {
            var templateId = id ?? "$/templates/" + Guid.NewGuid().ToString();

            db.FileStorage.Upload(templateId, name, stream);
        }

        public static void DeleteTemplate(string id)
        {
            db.FileStorage.Delete(id);
        }

        public static string GetTemplate(string id)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                db.FileStorage.Download(id, ms);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        public static List<Template> GetTemplates()
        {
            var files = db.FileStorage.FindAll();
            return files?.Select(file => new Template { Id = file.Id, Name = file.Filename }).ToList();
        }
    }

}

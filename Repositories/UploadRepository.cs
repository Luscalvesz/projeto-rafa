using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace API.Repositories {
    public class UploadRepository {
        public string Upload(IFormFile arquivo, string savingFolder) {

            if (savingFolder == null) {
                savingFolder = Path.Combine("imgUpdated");
            }

            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), savingFolder);

            if (arquivo.Length > 0) {
                var fileName = ContentDispositionHeaderValue.Parse(arquivo.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create)) {
                    arquivo.CopyTo(stream);
                }

                return fullPath;
            }
            else {
                return null;
            }
        }
    }
}

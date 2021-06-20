using IDSTORE2.Services;
using System;
using System.IO;
using System.Text;
using Xunit;

namespace TestIDStore
{
    public class ArchiveTest
    {
        private readonly ArchivesServices Services;
        private String ArchivePath = "C:\\RessourceFile\\Archives\\";
        private String NameArchiveFile;

        public ArchiveTest()
        {
           Services = new ArchivesServices(ArchivePath);

            try
            {
                // Create the file
                NameArchiveFile = "TestArchiveFile.pdf";
                using (FileStream fs = File.Create(ArchivePath + NameArchiveFile))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("TEST ARCHIVE FILE. this is some text in the file.");
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }



        [Fact]
        public void ArchiveUpdateFile_Test()
        {
            TypeArchives typeArchives = new TypeArchives();
            typeArchives = TypeArchives.Update;
            String classe = "B1";

            Services.ArchiveFile(typeArchives, ArchivePath + NameArchiveFile, classe,"");
        }
       
    }
}

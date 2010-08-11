using System.IO;

namespace Tests.Extensions
{
    public static class StreamExtensions
    {
        public static void Save(this Stream source, Stream target)
        {
            Save(source, target, 0);
        }

        public static void Save(this Stream source, string path)
        {
            Save(source, path, 0);
        }

        public static void Save(this Stream source, string path, int bufferSize)
        {
            Save(source,
                 new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite),
                 bufferSize);
        }

        public static void Save(this Stream source, Stream target, int bufferSize)
        {
            if (bufferSize < 1) bufferSize = 4096;
            using (Stream sourceStream = source, targetStream = target)
            {
                byte[] buffer = new byte[bufferSize];
                int length;
                while ((length = sourceStream.Read(buffer, 0, bufferSize)) > 0)
                    targetStream.Write(buffer, 0, length);
            }
        }
    }
}

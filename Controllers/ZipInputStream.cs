namespace APIformbuilder.Controllers
{
    internal class ZipInputStream
    {
        private MemoryStream zipStream;

        public ZipInputStream(MemoryStream zipStream)
        {
            this.zipStream = zipStream;
        }

        internal ZipEntry GetNextEntry()
        {
            throw new NotImplementedException();
        }

        internal int Read(byte[] buffer, int v, int length)
        {
            throw new NotImplementedException();
        }
    }
}
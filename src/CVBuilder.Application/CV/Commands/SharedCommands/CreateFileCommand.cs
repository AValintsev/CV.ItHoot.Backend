namespace CVBuilder.Application.CV.Commands.SharedCommands
{
    public class CreateFileCommand
    {
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
    }
}

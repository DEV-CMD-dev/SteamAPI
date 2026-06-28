namespace Shared.Classes
{
    public class SystemRequirements
    {
        public string OS { get; set; }
        public string Processor { get; set; }
        public string Memory { get; set; }
        public string Graphics { get; set; }
        public string Storage { get; set; }

        public SystemRequirements(string os, string processor, string memory, string graphics, string storage)
        {
            OS = os;
            Processor = processor;
            Memory = memory;
            Graphics = graphics;
            Storage = storage;
        }
    }
}
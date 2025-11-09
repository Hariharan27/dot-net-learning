using System;
namespace SampleWebAPI.services
{
	public class ScopedGuidService : IScopedGuidInterface
	{
        private readonly string _guid;

		public ScopedGuidService()
		{
            _guid = Guid.NewGuid().ToString();
		}

        public string GetGuid()
        {
            return _guid;
        }
    }
}


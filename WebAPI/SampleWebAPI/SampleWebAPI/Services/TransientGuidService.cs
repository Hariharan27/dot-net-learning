using System;
namespace SampleWebAPI.services
{
	public class TransientGuidService : ITransientGuidInterface
    {
        private readonly string _guid;

		public TransientGuidService()
		{
            _guid = Guid.NewGuid().ToString();
		}

        public string GetGuid()
        {
            return _guid;
        }
    }
}


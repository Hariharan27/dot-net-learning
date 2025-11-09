using System;
namespace SampleWebAPI.services
{
	public class SingletonGuidService : ISingletonGuidInterface
    {
		private readonly string _guid;

		public SingletonGuidService()
		{
			_guid = Guid.NewGuid().ToString();
		}

        public string GetGuid()
        {
            return _guid;
        }
    }
}


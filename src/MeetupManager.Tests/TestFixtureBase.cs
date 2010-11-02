using NUnit.Framework;
using Rhino.Mocks;

namespace Tests.MeetupManager
{
    [TestFixture]
    public class TestFixtureBase
    {
        /// <summary>
        /// Generates a Stub Object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T S<T>() where T : class
        {
            return MockRepository.GenerateStub<T>();
        }

        /// <summary>
        /// Generates a Mock Object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T M<T>() where T : class
        {
            return MockRepository.GenerateMock<T>();
        }
    }
}

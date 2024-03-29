// <copyright file="AnimalTestAnimalClass.cs">Copyright ©  2018</copyright>
using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shelter;

namespace Shelter.Tests
{
    /// <summary>This class contains parameterized unit tests for Animal</summary>
    [PexClass(typeof(Animal))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class AnimalTestAnimalClass
    {
        /// <summary>Test stub for IsAdoptable()</summary>
        [PexMethod]
        public bool IsAdoptableTestIsAdoptable([PexAssumeNotNull]Animal target)
        {
            bool result = target.IsAdoptable();
            return result;
            // TODO: add assertions to method AnimalTestAnimalClass.IsAdoptableTestIsAdoptable(Animal)
        }
    }
}

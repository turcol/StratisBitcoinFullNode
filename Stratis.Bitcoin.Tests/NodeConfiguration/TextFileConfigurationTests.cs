﻿using Stratis.Bitcoin.Configuration;
using Xunit;

namespace Stratis.Bitcoin.Tests.NodeConfiguration
{
    public class TextFileConfigurationTests
    {
        /// <summary>
        /// Assert that command line arguments with no value assigned default to "1".
        /// </summary>
        [Fact]
        public void GetAllWithArrayArgs()
        {
            // Arrange
            var textFileConfiguration = new TextFileConfiguration(new[] { "test" });
            // Act
            string[] result = textFileConfiguration.GetAll("test");
            // Assert
            Assert.Equal("1", result[0]);
        }

        /// <summary>
        /// Assert that we can get all the default values of command line arguments with or without a dash prefixing the key.
        /// </summary>
        [Fact]
        public void GetAllWithArrayArgsNoAssignment()
        {
            // Arrange
            var textFileConfiguration = new TextFileConfiguration(new[] { "test", "-test" });
            // Act
            string[] result = textFileConfiguration.GetAll("test");
            // Assert
            Assert.Equal(2, result.Length);
            Assert.Equal("1", result[0]);
            Assert.Equal("1", result[1]);
        }

        /// <summary>
        /// Assert that the parsing of command line arguments does not support spaces on each side of the = sign.
        /// </summary>
        [Fact]
        public void FailsToGetAllKeysWithArrayArgsAssignmentWithSpaces()
        {
            // Arrange
            var textFileConfiguration = new TextFileConfiguration(new[] { "test = testValue1", "-test = testValue2" });
            // Act
            string[] result = textFileConfiguration.GetAll("test");
            // Assert
            Assert.Equal(0, result.Length);
        }

        /// <summary>
        /// Assert that we can get all the assigned values of command line arguments with or without a dash prefixing the key.
        /// </summary>
        [Fact]
        public void GetAllKeysWithArrayArgsAssignment_NoSpace_()
        {
            // Arrange
            var textFileConfiguration = new TextFileConfiguration(new[] { "test=testValue1", "-test=testValue2" });
            // Act
            string[] result = textFileConfiguration.GetAll("test");
            // Assert
            Assert.Equal(2, result.Length);
            Assert.Equal("testValue1", result[0]);
            Assert.Equal("testValue2", result[1]);
        }

        /// <summary>
        /// Assert that we can get all the assigned values of arguments contained in a file with or without a dash prefixing the key.
        /// </summary>
        [Fact]
        public void GetAllSuccessMultipleKeysWithStringArgs()
        {
            // Arrange
            var textFileConfiguration = new TextFileConfiguration("test = testValue \n\r -test = testValue2");
            // Act
            string[] result = textFileConfiguration.GetAll("test");
            // Assert
            Assert.Equal(2, result.Length);
            Assert.Equal("testValue", result[0]);
            Assert.Equal("testValue2", result[1]);
        }
    }
}

// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using FluentAssertions;
using HexagonalApi.Business.Commands;
using HexagonalApi.Business.Validation;
using System;
using Xunit;

namespace HexagonalApi.Tests.Business
{
    public class SendpayloadCommandShould
    {
        [Fact(DisplayName = "number should not be empty")]
        public void Test1()
        {
            var validator = new SendPayloadCommandValidator();
            var sendpayloadCommand = new SendPayloadCommand(Number: String.Empty, Amount: 5);

            validator.Validate(sendpayloadCommand).IsValid.Should().BeFalse();
        }

        [Fact(DisplayName = "amount should not be empty")]
        public void Test2()
        {
            var validator = new SendPayloadCommandValidator();
            var sendpayloadCommand = new SendPayloadCommand("5", default(double));

            validator.Validate(sendpayloadCommand).IsValid.Should().BeFalse();
        }
    }
}

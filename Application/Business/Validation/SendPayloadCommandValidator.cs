// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using HexagonalApi.Business.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HexagonalApi.Business.Validation
{
    public class SendPayloadCommandValidator : AbstractValidator<SendPayloadCommand>
    {
        public SendPayloadCommandValidator()
        {
            RuleFor(x => x.Amount)
                .NotEmpty();

            RuleFor(x => x.Number)
                .NotEmpty();
        }

        /// will need to add validation like this

        //private bool HasValidCharacters(string number)
        //{
        //    var numbersAndSharpCharacter = new Regex("[0-9]|[#]+");

        //    return (numbersAndSharpCharacter.IsMatch(number));
        //}
    }
}

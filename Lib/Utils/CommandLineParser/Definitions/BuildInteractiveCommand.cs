﻿using Lib.Utils.CommandLineParser.Parser;

namespace Lib.Utils.CommandLineParser.Definitions
{
    public class BuildInteractiveCommand : CommonInteractiveCommand
    {
        public override string[] Words => new[] { "", "i", "interactive" };

        protected override string Description => "runs web controlled build ui";

        public CommandLineArgumentString Port { get; } = new CommandLineArgumentString(description: "set port for server to listen to", words: new[] { "-p", "--port" }, defaultValue: "8080");

        public CommandLineArgumentBool Sprite { get; } = new CommandLineArgumentBool(description: "enable/disable creation of sprites", words: new[] { "--sprite" }, defaultValue: false);
    }
}

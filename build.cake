#load ".cake/Configuration.cake"
#load ".cake/CI.cake"
#load ".cake/Restore-DotNetCore.cake"
#load ".cake/Build-DotNetCore.cake"
#load ".cake/Test-DotNetCore.cake"
#load ".cake/Publish-Pack-DotNetCore.cake"

Setup<Configuration>(Configuration.Create);
RunTarget(Argument("target", Argument("Target", "Default")));
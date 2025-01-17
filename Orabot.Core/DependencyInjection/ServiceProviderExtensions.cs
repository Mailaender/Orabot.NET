﻿using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orabot.Core.Abstractions.EventHandlers;
using Orabot.Core.EventHandlers;
using Orabot.Core.EventHandlers.CustomMessageHandlers.AttachmentMessageHandlers;
using Orabot.Core.EventHandlers.CustomMessageHandlers.CommandMessageHandlers;
using Orabot.Core.EventHandlers.CustomMessageHandlers.LinkParsingMessageHandlers;
using Orabot.Core.EventHandlers.CustomMessageHandlers.ModTimersMessageHandlers;
using Orabot.Core.EventHandlers.CustomMessageHandlers.NumberParsingMessageHandlers;
using Orabot.Core.EventHandlers.CustomMessageHandlers.NumberParsingMessageHandlers.GitHubIssueNumberMessageHandlers;
using Orabot.Core.EventHandlers.CustomMessageHandlers.SpecificTextMessageHandlers;
using Orabot.Core.Transformers.AttachmentToMessageTransformers;
using Orabot.Core.Transformers.LinkToEmbedTransformers;
using Orabot.Core.Transformers.Replays.ReplayDataToEmbedTransformers;
using Orabot.Core.Transformers.Replays.ReplayToReplayDataTransformers;

namespace Orabot.Core.DependencyInjection
{
    public static class ServiceProviderExtensions
	{
		public static IServiceCollection AddAppSettingsConfiguration(this IServiceCollection serviceCollection)
		{
			return serviceCollection
				.AddSingleton<IConfiguration>(provider =>
				{
					var builder = new ConfigurationBuilder()
						.SetBasePath(Directory.GetCurrentDirectory())
						.AddJsonFile("appsettings.json");

					return builder.Build();
				});
		}

		public static IServiceCollection AddDefaultEventHandlerServices(this IServiceCollection serviceCollection)
		{
			return serviceCollection
				.AddSingleton<ILogEventHandler, LogEventHandler>()
				.AddSingleton<IMessageEventHandler, MessageEventHandler>()
				.AddSingleton<IReactionEventHandler, RoleAssignmentReactionEventHandler>();
		}

		public static IServiceCollection AddDefaultCustomMessageHandlers(this IServiceCollection serviceCollection)
        {
	        return serviceCollection
		        .AddSingleton<ICustomMessageHandler, OpenRaGitHubIssueNumberMessageHandler>()
		        .AddSingleton<ICustomMessageHandler, OpenRaModSdkGitHubIssueNumberMessageHandler>()
		        .AddSingleton<ICustomMessageHandler, OpenRaRa2GitHubIssueNumberMessageHandler>()
		        .AddSingleton<ICustomMessageHandler, OpenRaWebGitHubIssueNumberMessageHandler>()
		        .AddSingleton<ICustomMessageHandler, OrabotGitHubIssueNumberMessageHandler>()
		        .AddSingleton<ICustomMessageHandler, OpenRaResourceCenterMapNumberMessageHandler>()
		        .AddSingleton<ICustomMessageHandler, OpenRaResourceCenterMapLinkMessageHandler>()
		        .AddSingleton<ICustomMessageHandler, LogFileAttachmentMessageHandler>()
		        .AddSingleton<ICustomMessageHandler, ReplayFileAttachmentMessageHandler>()
		        .AddSingleton<ICustomMessageHandler, BaseCommandMessageHandler>()
		        .AddSingleton<ICustomMessageHandler, BaseModTimerMessageHandler>()
		        .AddSingleton<ICustomMessageHandler, StackTraceMessageHandler>();
        }
		public static IServiceCollection AddDefaultTransformers(this IServiceCollection serviceCollection)
		{
			return serviceCollection
				.AddSingleton<AttachmentLogFileToMessageTransformer>()
				.AddSingleton<OpenRaResourceCenterMapLinkToEmbedTransformer>()
				.AddSingleton<AttachmentReplayToUtilityMetadataTransformer>()
				.AddSingleton<UtilityReplayMetadataToEmbedTransformer>();
		}
    }
}

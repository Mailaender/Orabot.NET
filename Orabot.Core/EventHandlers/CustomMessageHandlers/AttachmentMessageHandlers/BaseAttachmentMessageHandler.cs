﻿using System.Linq;
using Discord.WebSocket;
using Orabot.Core.Abstractions.EventHandlers;

namespace Orabot.Core.EventHandlers.CustomMessageHandlers.AttachmentMessageHandlers
{
	internal abstract class BaseAttachmentMessageHandler : ICustomMessageHandler
	{
		protected abstract string FileExtension { get; }

		public virtual bool CanHandle(SocketUserMessage message)
		{
			return message.Attachments.Count != 0 && message.Attachments.Any(x => x.Filename.EndsWith(FileExtension));
		}

		public abstract void Invoke(SocketUserMessage message);
	}
}

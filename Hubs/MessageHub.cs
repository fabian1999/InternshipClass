﻿using Microsoft.AspNetCore.SignalR;
using RazorMvc.Models;
using RazorMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorMvc.Hubs
{
    public class MessageHub : Hub, IAddMemberSubscriber
    {
        private readonly IInternshipService internshipService;
        private readonly MessageService messageService;

        public MessageHub(MessageService messageService, IInternshipService internshipService)
        {
            this.internshipService = internshipService;
            this.messageService = messageService;
            internshipService.SubscribeToAddMember(this);
        }

        public async void OnAddMember(Intern member)
        {
            await Clients.All.SendAsync("AddMember", member.Name, member.Id);
        }

        public async Task SendMessage(string user, string message)
        {
            messageService.AddMessage(user, message);
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}

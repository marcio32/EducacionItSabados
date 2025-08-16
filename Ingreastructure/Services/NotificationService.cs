using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Services
{
    internal class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(IHubContext<NotificationHub> hubContext) => _hubContext = hubContext;

        public async Task NotifyTurnoCreated(int turnoId, string pacienteNombre, string medicoNombre)
        {
            await _hubContext.Clients.All.SendAsync("TurnoCreated", new
            {
                TurnoId = turnoId,
                Message = $"Nuevo turno creado para {pacienteNombre} con Dr. {medicoNombre}."
            });
        }
        public async Task NotifyTurnoCanceled(int turnoId, string motivo)
        {
            await _hubContext.Clients.All.SendAsync("TurnoCanceled", new
            {
                TurnoId = turnoId,
                Message = $"El Turno #{turnoId} fue cancelado por {motivo}"
            });
        }

        public async Task NotifyTurnoUpdated(int turnoId, string estado)
        {
            await _hubContext.Clients.All.SendAsync("TurnoUpdated", new
            {
                TurnoId = turnoId,
                Message = $"Turno #{turnoId} cambio a estado {estado}"
            });
        }

        public async Task SendMessageToUser(string userId, string message)
        {
            await _hubContext.Clients.User(userId).SendAsync("ReceiveMessage", message);
        }

        public async Task UpdateDashboard()
        {
            await _hubContext.Clients.All.SendAsync("UpdateDashboard");
        }
    }
}

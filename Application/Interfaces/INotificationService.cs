using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface INotificationService
    {
        Task NotifyTurnoCreated(int turnoId, string pacienteNombre, string medicoNombre);
        Task NotifyTurnoUpdated(int turnoId, string estado);
        Task NotifyTurnoCanceled(int turnoId, string motivo);
        Task SendMessageToUser(string userId, string message);
        Task UpdateDashboard();
    }
}

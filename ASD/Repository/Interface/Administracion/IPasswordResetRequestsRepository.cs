using System;
using ASD.Areas.Administracion.Models;

namespace ASD.Repository.Interface.Administracion
{
	public interface IPasswordResetRequestsRepository
	{
        //PC
        Task<PasswordResetRequestsViewModel> PasswordResetRequests_Insert(PasswordResetRequestsViewModel _model);
        Task<PasswordResetRequestsViewModel> PasswordResetRequests_Consult(PasswordResetRequestsViewModel _model);
        Task<PasswordResetRequestsViewModel> Usuario_ActualizarPass(PasswordResetRequestsViewModel _model);


        //MOVIL
        Task<PasswordResetRequestsViewModel> PasswordResetRequests_InsertM(PasswordResetRequestsViewModel _model);
        Task<PasswordResetRequestsViewModel> PasswordResetRequests_ConsultM(PasswordResetRequestsViewModel _model);
        Task<PasswordResetRequestsViewModel> Usuario_ActualizarPassM(PasswordResetRequestsViewModel _model);

    }
}


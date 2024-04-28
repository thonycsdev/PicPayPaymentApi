using Application.AutoMapper;
using Application.UseCases.Usuarios.AdicionarSaldo;
using Application.UseCases.Usuarios.ChangePassword;
using Application.UseCases.Usuarios.Create;
using Application.UseCases.Usuarios.Delete;
using Application.UseCases.Usuarios.GetAll;
using Application.UseCases.Usuarios.GetById;
using Application.UseCases.Usuarios.Transferencia;
using Application.UseCases.Usuarios.Update;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection.RepositoryDI
{
    public static class UseCaseServiceInjection
    {
        public static void AddUseCases(this IServiceCollection service)
        {
            //usuario
            service.AddScoped<IAdicionarSaldoUseCase, AdicionarSaldoUseCase>();
            service.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
            service.AddScoped<ICreateUsuarioUseCase, CreateUsuarioUseCase>();
            service.AddScoped<IDeleteUsuarioUseCase, DeleteUsuarioUseCase>();
            service.AddScoped<IGetAllUsuariosUseCase, GetAllUsuariosUseCase>();
            service.AddScoped<IUpdateUsuarioUseCase, UpdateUsuarioUseCase>();
            service.AddScoped<ITransferenciaSaldoUseCase, TransferenciaUseCase>();

            //auto mapper
            service.AddAutoMapper(typeof(AutoMapperConfigProfile));
            //mediatr
            //
            service.AddMediatR(conf =>
                conf.RegisterServicesFromAssembly(typeof(GetUsuarioByIdQuery).Assembly)
            );
        }
    }
}

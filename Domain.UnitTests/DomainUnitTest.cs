using Domain.Entities;
using Domain.Enums;

namespace Domain.UnitTests
{
    public class EstadosTurnoTests
    {
        [Fact]
        public void EstadoTurno_CorrectValues()
        {
            Assert.Equal(1, (int)EstadoTurno.Pendiente);
            Assert.Equal(2, (int)EstadoTurno.Confirmado);
            Assert.Equal(3, (int)EstadoTurno.Cancelado);
            Assert.Equal(4, (int)EstadoTurno.Realizado);
            Assert.Equal(5, (int)EstadoTurno.NoPresentado);
            Assert.Equal(6, (int)EstadoTurno.Reprogramado);
            Assert.Equal(7, (int)EstadoTurno.Rechazado);
        }
    }

    public class TurnosEntityTests
    {
        [Fact]
        public void TUrnos_CreateWithDefaultValues()
        {
            var turno = new Turnos();
            Assert.Equal(0, turno.Id);
            Assert.Equal(DateTime.MinValue, turno.FechaHora);
            Assert.Null(turno.Observaciones);
            Assert.NotNull(turno.Documentos);
            Assert.Empty(turno.Documentos);
            Assert.Equal(0, turno.MedicoId);
            Assert.Equal(0, turno.UsuarioId);
            Assert.Equal(0, turno.PacienteId);
            Assert.Equal(0, turno.EstadoId);
            Assert.Equal(0, turno.DocumentosId);
            Assert.Equal(0, turno.EstudioId);
            Assert.Null(turno.FechaModificacion);
            Assert.Null(turno.Estado);
            Assert.Null(turno.Medico);
            Assert.Null(turno.Usuario);
            Assert.Null(turno.Paciente);
            Assert.Null(turno.Estudio);
        }

        [Fact]
        public void Turnos_SetProperties()
        {
            var fechaHora = DateTime.Now.AddDays(1);
            var turno = new Turnos
            {
                Id = 1,
                FechaHora = fechaHora,
                MedicoId = 1,
                UsuarioId = 1,
                PacienteId = 1,
                EstadoId = (int)EstadoTurno.Confirmado,
                Observaciones = "Test",
            };

            Assert.Equal(1, turno.Id);
            Assert.Equal(fechaHora, turno.FechaHora);
            Assert.Equal(1, turno.MedicoId);
            Assert.Equal(1, turno.UsuarioId);
            Assert.Equal(1, turno.PacienteId);
            Assert.Equal((int)EstadoTurno.Confirmado, turno.EstadoId);
            Assert.Equal("Test", turno.Observaciones);
        }
    }

    public class PacientesEntityTests
    {
        [Fact]
        public void Pacientes_CreateWithRequiredProperties()
        {
            var paciente = new Pacientes
            {
                Id = 1,
                Nombre = "Juan",
                Apellido = "Perez",
                Dni = 12345678,
                FechaNacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)),
                Telefono = 123456789,
                Email = "juan@example.com"
            };

            Assert.Equal(1, paciente.Id);
            Assert.Equal("Juan", paciente.Nombre);
            Assert.Equal("Perez", paciente.Apellido);
            Assert.Equal(12345678, paciente.Dni);
            Assert.Equal(DateOnly.FromDateTime(DateTime.Now.AddYears(-30)), paciente.FechaNacimiento);
            Assert.Equal(123456789, paciente.Telefono);
            Assert.Equal("juan@example.com", paciente.Email);
        }
    }

    public class MedicosEntityTest
    {
        [Fact]
        public void Medicos_CreateWithRequiredProperties()
        {
            var medico = new Medicos
            {
                Id = 1,
                Nombre = "Dr. Carlos",
                Apellido = "Gomez",
                EspecialidadId = 1,
                FechaNacimiento = DateTime.Now.AddYears(-30).Date,
                Email = "carlos@test.com"
            };

            Assert.Equal(1, medico.Id);
            Assert.Equal("Dr. Carlos", medico.Nombre);
            Assert.Equal("Gomez", medico.Apellido);
            Assert.Equal(1, medico.EspecialidadId);
            Assert.Equal(DateTime.Now.AddYears(-30).Date, medico.FechaNacimiento);
            Assert.Equal("carlos@test.com", medico.Email);

        }
    }
    public class DocumentosEntityTest
    {
        [Fact]
        public void Documentos_CreateWithRequiredProperties()
        {
            var documento = new Documentos
            {
                Id = 1,
                Nombre = "Documento1",
                Ruta = "/path/to/documento1.pdf",
                TurnosId = 1,
                FechaSubida = DateTime.Now.Date
            };
            Assert.Equal(1, documento.Id);
            Assert.Equal("Documento1", documento.Nombre);
            Assert.Equal("/path/to/documento1.pdf", documento.Ruta);
            Assert.Equal(1, documento.TurnosId);
            Assert.Equal(DateTime.Now.Date, documento.FechaSubida);

        }
    }
    public class EstadosEntityTest
    {
        [Fact]
        public void Estados_CreateWithRequiredProperties()
        {
            var estado = new Estados
            {
                Id = 1,
                Nombre = "Pendiente",
                Estado = true
            };
            Assert.Equal(1, estado.Id);
            Assert.Equal("Pendiente", estado.Nombre);
            Assert.True(estado.Estado);
        }
    }
    public class EstudioEntityTest
    {
        [Fact]
        public void Estudio_CreateWithRequiredProperties()
        {
            var estudio = new Estudios
            {
                Id = 1,
                Nombre = "Estudio1",
                Descripcion = "Descripcion del estudio 1",
                Estado = true
            };
            Assert.Equal(1, estudio.Id);
            Assert.Equal("Estudio1", estudio.Nombre);
            Assert.Equal("Descripcion del estudio 1", estudio.Descripcion);
            Assert.True(estudio.Estado);
        }
    }
    public class RolesEntityTest
    {
        [Fact]
        public void Roles_CreateWithRequiredProperties()
        {
            var rol = new Roles
            {
                Id = 1,
                Nombre = "Admin",
                Estado = true
            };
            Assert.Equal(1, rol.Id);
            Assert.Equal("Admin", rol.Nombre);
            Assert.True(rol.Estado);
        }
    }
    public class ServiciosEntityTest
    {
        [Fact]
        public void Servicios_CreateWithRequiredProperties()
        {
            var servicio = new Servicios
            {
                Id = 1,
                Nombre = "Servicio1",
                Descripcion = "Descripcion del servicio 1",
                Estado = true
            };
            Assert.Equal(1, servicio.Id);
            Assert.Equal("Servicio1", servicio.Nombre);
            Assert.Equal("Descripcion del servicio 1", servicio.Descripcion);
            Assert.True(servicio.Estado);
        }
    }
    public class EspecialidadesEntityTest
    {
        [Fact]
        public void Especialidades_CreateWithRequiredProperties()
        {
            var especialidad = new Especialidades
            {
                Id = 1,
                Nombre = "Cardiologia",
                Estado = true
            };

            Assert.Equal(1, especialidad.Id);
            Assert.Equal("Cardiologia", especialidad.Nombre);
            Assert.True(especialidad.Estado);
        }
    }
    public class UsuariosEntityTest
    {
        [Fact]
        public void Usuarios_CreateWithRequiredProperties()
        {
            var usuario = new Usuarios
            {
                Id = 1,
                Nombre = "Usuario1",
                Email = "test@gmail.com",
                HashPassword = "hashedpassword",
                RolId = 1,
                Rol = new Roles
                {
                    Id = 1,
                    Nombre = "Admin",
                    Estado = true
                },
                Estado = true,
                FechaCreacion = DateTime.Now.Date,
                FechaModificacion = DateTime.Now.Date,
                Codigo = 1,

            };
            Assert.Equal(1, usuario.Id);
            Assert.Equal("Usuario1", usuario.Nombre);
            Assert.Equal("test@gmail.com", usuario.Email);
            Assert.Equal("hashedpassword", usuario.HashPassword);
            Assert.Equal(1, usuario.RolId);
            Assert.NotNull(usuario.Rol);
            Assert.Equal("Admin", usuario.Rol?.Nombre);
            Assert.True(usuario.Estado);
            Assert.Equal(DateTime.Now.Date, usuario.FechaCreacion);
            Assert.Equal(DateTime.Now.Date, usuario.FechaModificacion);
            Assert.Equal(1, usuario.Codigo);
        }
    }
}
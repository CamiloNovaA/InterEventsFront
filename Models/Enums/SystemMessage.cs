namespace Models.Enums
{
    public static class SystemMessage
    {
        public const string RegistrySuccess = "Registro exitoso.";
        public const string EmailExist = "El correo ingresado ya se encuentra registrado.";
        public const string EventCreated = "Se creo el evento con éxito.";
        public const string EventEdited = "Se actualizo el evento con éxito.";
        public const string EventCannotEdit = "Este evento no se puede editar, contacte con el administrador.";
        public const string EventEliminated = "Se elimino el evento con éxito.";
        public const string EventCannotEliminate = "Este evento no se puede eliminar, contacte con el administrador.";
        public const string EventHasAttendants = "Este evento no se puede eliminar por que tiene asistentes registrados.";
        public const string EventInactive = "Se desactivo el evento.";
        public const string EventActive = "Se activo el evento.";
        public const string EventCannotChangeState = "A este evento no se le puede cambiar el estado, contacte con el administrador.";
        public const string CannotSuscribeForEvent = "No se puede registrar en este evento, contacte con el administrador.";
        public const string CannotSuscribeForLimit = "No se puede registrar en este evento, por que ya llego al limite de asistentes.";
        public const string SuscribeAttendantForEvent = "Tú registro se completo con exito.";
        public const string CannotUnsuscribeForEvent = "Lo sentimos, no se puede desuscribir de este vento, contacte con el administrador.";
        public const string UnsuscribeAttendantForEvent = "Has dejado de participar en el evento.";
        public const string RegistryWithLimitForEvents = "Has superado el limite de registros a eventos, solo puedes registrarte en 3 eventos.";
        public const string RegistryEventOfMyProperty = "Lo sentimos, no se puede registrar a un evento propio.";
    }
}

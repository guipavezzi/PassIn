﻿using PassIn.Infrastructure.Entities;

public class CheckIn
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Created_at { get; set; }

    public Guid AttendeeId { get; set; } 
    public Attendee Attendee { get; set; } = default!;
}

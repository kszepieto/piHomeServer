﻿using System;
using System.Collections.Generic;
using piHome.Models.ValueObjects;

namespace piHome.Models.Entities.UserSettings
{
    public class CircuitsHandlingSetEntity : BaseEntity
    {
        public string Name { get; set; }
        public bool IsPrivate { get; set; }
        public List<StateChange> StatesOnActivation { get; set; }
        public List<StateChange> StatesOnDeactivation { get; set; }
        public bool IsEnabled { get; set; }
        public string Owner { get; set; }

        public List<string> GetValidationErrors()
        {
            throw new NotImplementedException();
        }
    }
}

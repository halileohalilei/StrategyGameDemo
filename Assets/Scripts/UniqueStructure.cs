﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class UniqueStructure : Structure
    {
        public override int GetStructureType()
        {
            return (int) StructureType.Unique;
        }

        public override void OpenPopUp()
        {
            base.OpenPopUp();
            Debug.Log("UniqueStructure");
        }
    }
}
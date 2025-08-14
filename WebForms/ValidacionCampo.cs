using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace WebForms.Utils
{
    public static class ValidacionCampo
    {
      
        public static bool ValidarCorreo(string correo)
        {
            
            string patron = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(correo, patron);
        }

        public static void ControlAceptar(Button boton, WebControl[] controles)
        {
            if (boton == null)
                throw new ArgumentNullException(nameof(boton));

            boton.Enabled = TodosCamposCompletos(controles);
        }

      
        public static bool TodosCamposCompletos(WebControl[] controles)
        {
            if (controles == null)
                throw new ArgumentNullException(nameof(controles));

            return controles.All(control =>
            {
                if (control == null) return false;

                if (control is TextBox textBox)
                {
                    return !string.IsNullOrWhiteSpace(textBox.Text);
                }
                else if (control is DropDownList dropDown)
                {
                    // Validar que tenga un ítem seleccionado y que no sea el valor por defecto
                    return dropDown.SelectedIndex > 0; // Asume que el índice 0 es el valor por defecto como "--Seleccione--"
                }

                return true; // Si no es TextBox ni DropDownList, lo consideramos válido
            });
        }

        public static bool ValidarComision(string comision)
        {
            
            string patron = @"^(\d{1,2}(\.\d{1,2})?%|0(\.\d{1,2})?)$";
            return Regex.IsMatch(comision, patron);
        }


    }

}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Dominio;

namespace Presentacion
{
    public partial class LoginFerreteriaMaresa : Form
    {
        public LoginFerreteriaMaresa()
        {
            InitializeComponent();
        }

        private void btCnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void LoginFerreteriaMaresa_Enter(object sender, EventArgs e)
        {
        }

        private void LoginFerreteriaMaresa_Leave(object sender, EventArgs e)
        {
        }

        private void txtContra_Enter(object sender, EventArgs e)
        {
            if (txtContra.Text == "CONTRASEÑA")
            {
                txtContra.Text = "";
                txtContra.ForeColor = Color.LightGray;
                txtContra.UseSystemPasswordChar = true;
            }
        }

        private void txtContra_Leave(object sender, EventArgs e)
        {
            if (txtContra.Text =="")
            {
                txtContra.Text = "CONTRASEÑA";
                txtContra.ForeColor = Color.DimGray;
                txtContra.UseSystemPasswordChar = false;
            }
        }
        [DllImport("user32.Dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.Dll", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hwnd, int wMsg, int wParam, int lParam);
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void LoginFerreteriaMaresa_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnAcceder_Click(object sender, EventArgs e)
        {
            DOM_Empleados emp = new DOM_Empleados();
            DataTable tabla = emp.autentificacion_empleado(txtUsuario.Text, txtContra.Text);
            if (tabla.Rows.Count > 0)
            {
                switch ((int)tabla.Rows[0][5])
                {
                    case 1:
                        {

                            MenuAdmin adm = new MenuAdmin();
                            adm.Show();
                            Hide();
                            break;
                        }
                    case 2:
                        {
                            MenuEmpleados empl = new MenuEmpleados();
                            empl.Show();
                            Hide();
                            break;
                        }
                    default:
                        MessageBox.Show("Error! Usuario o Contraseña incorrecta intente de nuevo", "Mensaje de Error", MessageBoxButtons.OK);
                        break;
                }
            }

            /* 
             if (tabla.Rows.Count > 0)
             {
                 switch ((int)tabla.Rows[0][10])
                 {
                     case 1:
                         {

                             MenuAdmin adm = new MenuAdmin();
                             adm.Show();
                             Hide();
                             break;
                         }
                     case 2:
                         {
                             MenuEmpleados empl = new MenuEmpleados();
                             empl.Show();
                             Hide();
                             break;
                         }
                     default:
                         MessageBox.Show("a");
                         break;
                 }
                 MessageBox.Show("Error! Usuario o Contraseña incorrecta intente de nuevo", "Mensaje de Error", MessageBoxButtons.OK);
             }
             */

        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "USUARIO")
            {
                txtUsuario.Text = "";
                txtUsuario.ForeColor = Color.LightGray;
            }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                txtUsuario.Text = "USUARIO";
                txtUsuario.ForeColor = Color.DimGray;
            }
        }

        private void LoginFerreteriaMaresa_Load(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.FreeGlut;
using Tao.OpenGl;
using System.IO.Ports;
using System.Threading;

namespace TiroAoAlvo
{
    class Program
    {
        static SerialPort porta = new SerialPort("COM3");
        static Boolean dia = true;
        const float PI = 3.14159265358f;
        static float tx = 0.0f;
        static float tm = 0.0f;
        static float rot = 0.0f;
        static int sinal = 0;
        static bool bit = false;
        static bool bitAcerto = false;
        static bool bitErro = false;
        static int tem1 = 0, tem2 = 0, tem3 = 0, tem4 = 0;
        const int delay = 1000;

        static void fundoDia()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);//Limpa a janela de visualização com a cor de fundo especificada

            Gl.glBegin(Gl.GL_QUADS);//Inicia fazer quadrados onde será colorido de acordo com a cor abaixo
            Gl.glColor3f(0.282353f, 0.239216f, 0.545098f);//céu
            Gl.glVertex2f(1.0f, 1.0f);
            Gl.glVertex2f(0.0f, 1.0f);
            Gl.glColor3f(0.0f, 0.74902f, 1.0f);
            Gl.glVertex2f(0.0f, 0.6f);
            Gl.glVertex2f(1.0f, 0.6f);
            Gl.glColor3f(0, 0.239216f, 0);//Gramado
            Gl.glVertex2f(1.0f, 0.6f);
            Gl.glVertex2f(0.0f, 0.6f);
            Gl.glColor3f(0, 0.74902f, 0);
            Gl.glVertex2f(0.0f, 0.0f);
            Gl.glVertex2f(1.0f, 0.0f);
            Gl.glEnd();

            muroDia();
            hastes();
            refletorDia();

            if (bitAcerto)
            {
                Acerto();
            } else
                if (bitErro)
                {
                    Erro();
                }
        }

        static void fundoNoite()
        {

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);//Limpa a janela de visualização com a cor de fundo especificada

            Gl.glBegin(Gl.GL_QUADS);//Inicia fazer quadrados onde será colorido de acordo com a cor abaixo
            Gl.glColor3f(0.2f, 0.1f, 0.3f);//céu
            Gl.glVertex2f(1.0f, 1.0f);
            Gl.glVertex2f(0.0f, 1.0f);
            Gl.glColor3f(0.0f, 0.0f, 0.0f);
            Gl.glVertex2f(0.0f, 0.6f);
            Gl.glVertex2f(1.0f, 0.6f);
            Gl.glColor3f(0, 0.139216f, 0);//Gramado
            Gl.glVertex2f(1.0f, 0.6f);
            Gl.glVertex2f(0.0f, 0.6f);
            Gl.glColor3f(0, 0.64902f, 0);
            Gl.glVertex2f(0.0f, 0.0f);
            Gl.glVertex2f(1.0f, 0.0f);
            Gl.glEnd();

            //Estelas
            Gl.glColor3f(0.3f, 0.3f, 0.0f); // amarelo
            Gl.glPointSize(5.0f); // aumenta o tamanho dos pontos
            Gl.glBegin(Gl.GL_POINTS);
            Gl.glVertex2f(0.1f, 0.9f);
            Gl.glVertex2f(0.2f, 0.7f);
            Gl.glVertex2f(0.9f, 0.95f);
            Gl.glVertex2f(0.5f, 0.95f);
            Gl.glVertex2f(0.9f, 0.68f);
            Gl.glVertex2f(0.55f, 0.75f);
            Gl.glEnd();

            muroNoite();
            hastes();
            refletorNoite();

            if (bitAcerto)
            {
                Acerto();
            }
            else
                if (bitErro)
            {
                Erro();
            }
        }

        static void hastes()
        {
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glColor3f(0.0f, 0.0f, 0.0f);
            Gl.glVertex2f(0.7f, 0.89f);//haste à direita
            Gl.glVertex2f(0.7f, 0.59f);
            Gl.glVertex2f(0.67f, 0.59f);
            Gl.glVertex2f(0.67f, 0.89f);
            Gl.glVertex2f(0.7f, 0.89f);//haste central
            Gl.glVertex2f(0.3f, 0.89f);
            Gl.glVertex2f(0.3f, 0.86f);
            Gl.glVertex2f(0.7f, 0.86f);
            Gl.glVertex2f(0.3f, 0.89f);//haste à esquerda
            Gl.glVertex2f(0.3f, 0.59f);
            Gl.glVertex2f(0.33f, 0.59f);
            Gl.glVertex2f(0.33f, 0.89f);
            Gl.glEnd();
        }

        static void atirador()
        {
            float raio, x, y, pontos;
            raio = 0.01f;
            pontos = (2 * PI) / 1000;

            Gl.glPushMatrix();
            Gl.glTranslatef(tx, 0, 0);
            Gl.glColor3f(0.5f, 1.0f, 0.0f);
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glColor3f(0.0f, 0.0f, 0.2f);
            Gl.glVertex2f(0.51f, 0.0f);//Camisas
            Gl.glVertex2f(0.57f, 0.0f);
            Gl.glColor3f(0.3f, 0.3f, 0.5f);
            Gl.glVertex2f(0.54f, 0.09f);
            Gl.glVertex2f(0.50f, 0.09f);
            Gl.glColor3f(0.0f, 0.0f, 0.2f);
            Gl.glVertex2f(0.43f, 0.0f);//Camisas3
            Gl.glVertex2f(0.50f, 0.0f);
            Gl.glColor3f(0.4f, 0.4f, 0.5f);
            Gl.glVertex2f(0.52f, 0.09f);
            Gl.glVertex2f(0.48f, 0.09f);
            Gl.glEnd();
            Gl.glBegin(Gl.GL_POLYGON);
            Gl.glColor3f(0.3f, 0.2f, 0.0f);
            Gl.glVertex2f(0.52f, 0.09f);//Mãos
            Gl.glVertex2f(0.54f, 0.09f);
            Gl.glVertex2f(0.54f, 0.11f);
            Gl.glColor3f(0.5f, 0.2f, 0.0f);
            Gl.glVertex2f(0.48f, 0.09f);//Mãos
            Gl.glVertex2f(0.48f, 0.12f);
            Gl.glVertex2f(0.52f, 0.14f);
            Gl.glColor3f(0.4f, 0.2f, 0.1f);
            Gl.glVertex2f(0.54f, 0.11f);
            Gl.glVertex2f(0.52f, 0.09f);
            Gl.glEnd();
            Gl.glBegin(Gl.GL_POLYGON);
            Gl.glColor3f(0.0f, 0.0f, 0.0f);//Arma
            Gl.glVertex2f(0.50f, 0.11f);
            Gl.glVertex2f(0.50f, 0.16f);
            Gl.glVertex2f(0.515f, 0.155f);
            Gl.glVertex2f(0.525f, 0.16f);
            Gl.glVertex2f(0.525f, 0.11f);
            Gl.glColor3f(0.1f, 0.1f, 0);
            Gl.glVertex2f(0.505f, 0.20f);
            Gl.glVertex2f(0.515f, 0.20f);
            Gl.glVertex2f(0.52f, 0.16f);
            Gl.glEnd();

            Gl.glPushMatrix();//Faz mover apenas tm (tua mira)
            Gl.glTranslatef(0, tm, 0);
            Gl.glLineWidth(2);
            Gl.glBegin(Gl.GL_LINE_LOOP);
            Gl.glColor3f(0.0f, 1.0f, 0.0f);
            Gl.glVertex2f(0.5f, 0.87f);
            for (float angulo = 0; angulo <= 2 * PI; angulo += pontos)
            {
                x = (float)(raio * Math.Cos(angulo) + 0.5f);
                y = (float)(raio * Math.Sin(angulo) + 0.87f);
                Gl.glVertex2f(x, y);
            }
            Gl.glEnd();
            Gl.glPopMatrix();//devolve template original com a mira já alterada de lugar
            Gl.glPopMatrix();//devolve template original com o atirador já alterado de lugar
        }


        static void refletorDia()
        {
            float raio, x, y, pontos;
            raio = 0.03f;
            pontos = (2 * PI) / 1000;

            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glColor3f(0.0f, 0.0f, 0.0f);
            Gl.glVertex2f(0.5f, 0.89f);

            for (float angulo = 0; angulo <= 2 * PI; angulo += pontos)
            {
                x = (float)(raio * Math.Cos(angulo) + 0.5f);
                y = (float)(raio * Math.Sin(angulo) + 0.87f);
                Gl.glVertex2f(x, y);
            }
            Gl.glEnd();

            raio = 0.02f;
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glColor3f(0.3f, 0.3f, 0.3f);
            Gl.glVertex2f(0.5f, 0.88f);

            for (float angulo = 0; angulo <= 2 * PI; angulo += pontos)
            {
                x = (float)(raio * Math.Cos(angulo) + 0.5f);
                y = (float)(raio * Math.Sin(angulo) + 0.86f);
                Gl.glVertex2f(x, y);
            }
            Gl.glEnd();
        }

        static void refletorNoite()
        {
            float raio, x, y, pontos;
            raio = 0.03f;
            pontos = (2 * PI) / 1000;

            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glColor3f(0.0f, 0.0f, 0.0f);
            Gl.glVertex2f(0.5f, 0.89f);

            for (float angulo = 0; angulo <= 2 * PI; angulo += pontos)
            {
                x = (float)(raio * Math.Cos(angulo) + 0.5f);
                y = (float)(raio * Math.Sin(angulo) + 0.87f);
                Gl.glVertex2f(x, y);
            }
            Gl.glEnd();

            raio = 0.02f;

            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glColor3f(0.9f, 0.9f, 0.2f);
            Gl.glVertex2f(0.5f, 0.88f);

            for (float angulo = 0; angulo <= 2 * PI; angulo += pontos)
            {
                x = (float)(raio * Math.Cos(angulo) + 0.5f);
                y = (float)(raio * Math.Sin(angulo) + 0.86f);
                Gl.glVertex2f(x, y);
            }
            Gl.glEnd();
        }

        static void raiosLuz()
        {
            Gl.glPushMatrix();//serve para lembrar os parâmetros de translação, rotação e escalamento no início das operações de desenho

            Gl.glTranslatef(0.5f, 0.87f, 0.0f);//A origem do sistema de coordenadas é levado para o ponto (x,y,z)
            Gl.glRotatef(rot, 0.0f, 0.0f, 1.0f);//gira o objeto ao redor do vetor (x,y,z). O giro é de Angulo graus, no sentido anti-horário
            Gl.glTranslatef(-0.5f, -0.87f, 0.0f);//Permite a helice permanecer centralizada centre 
            Gl.glColor3f(0.9f, 0.9f, 0.2f);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex2f(0.5f, 0.87f);
            Gl.glVertex2f(0.45f, 0.87f);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex2f(0.5f, 0.87f);
            Gl.glVertex2f(0.5f, 0.82f);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex2f(0.5f, 0.87f);
            Gl.glVertex2f(0.55f, 0.87f);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex2f(0.5f, 0.87f);
            Gl.glVertex2f(0.5f, 0.92f);
            Gl.glEnd();

            Gl.glPopMatrix();//usado para não aplicar a rotação em todo o desenho, apenas na Helice (Devolve os parâmetros em pilha do layout
        }

        static void muroDia()
        {
            Gl.glBegin(Gl.GL_QUADS);  //muros
            Gl.glColor3f(0.6f, 0.5f, 0.6f);
            Gl.glVertex2f(0.7f, 0.59f);//Muro a direita
            Gl.glVertex2f(1.9f, -0.3f);
            Gl.glColor3f(0.0f, 0.2f, 0.3f);
            Gl.glVertex2f(1.9f, -0.91f);
            Gl.glVertex2f(0.7f, 0.52f);
            Gl.glColor3f(0.6f, 0.5f, 0.6f);
            Gl.glVertex2f(0.3f, 0.59f);//Muro Central            
            Gl.glVertex2f(0.7f, 0.59f);
            Gl.glColor3f(0.0f, 0.2f, 0.3f);
            Gl.glVertex2f(0.7f, 0.52f);
            Gl.glVertex2f(0.3f, 0.52f);
            Gl.glColor3f(0.6f, 0.5f, 0.6f);
            Gl.glVertex2f(0.3f, 0.59f);//Muro a esquerda
            Gl.glVertex2f(-1.2f, -0.45f);
            Gl.glColor3f(0.0f, 0.2f, 0.3f);
            Gl.glVertex2f(-1.2f, -1.2f);
            Gl.glVertex2f(0.3f, 0.52f);
            Gl.glEnd();
        }

        static void muroNoite()
        {
            Gl.glBegin(Gl.GL_QUADS);  //muros
            Gl.glColor3f(0.0f, 0.5f, 0.6f);
            Gl.glVertex2f(0.7f, 0.59f);//Muro a direita
            Gl.glVertex2f(1.9f, -0.3f);
            Gl.glColor3f(0.0f, 0.2f, 0.3f);
            Gl.glVertex2f(1.9f, -0.91f);
            Gl.glVertex2f(0.7f, 0.52f);
            Gl.glColor3f(0.0f, 0.5f, 0.6f);
            Gl.glVertex2f(0.3f, 0.59f);//Muro Central            
            Gl.glVertex2f(0.7f, 0.59f);
            Gl.glColor3f(0.0f, 0.2f, 0.3f);
            Gl.glVertex2f(0.7f, 0.52f);
            Gl.glVertex2f(0.3f, 0.52f);
            Gl.glColor3f(0.0f, 0.5f, 0.6f);
            Gl.glVertex2f(0.3f, 0.59f);//Muro a esquerda
            Gl.glVertex2f(-1.2f, -0.45f);
            Gl.glColor3f(0.0f, 0.2f, 0.3f);
            Gl.glVertex2f(-1.2f, -1.2f);
            Gl.glVertex2f(0.3f, 0.52f);
            Gl.glEnd();
        }

        static void alvo()
        {
            //Suporte do Arco
            Gl.glBegin(Gl.GL_QUADS);
            Gl.glColor3f(1, 0.3f, 0);
            Gl.glVertex2f(0.48f, 0.51f); // 
            Gl.glVertex2f(0.48f, 0.45f);//--
            Gl.glVertex2f(0.52f, 0.45f); //-
            Gl.glVertex2f(0.52f, 0.51f);//--

            Gl.glEnd();

            Gl.glBegin(Gl.GL_POLYGON);
            Gl.glColor3f(1, 0.3f, 0);
            Gl.glVertex2f(0.48f, 0.46f);
            Gl.glVertex2f(0.44f, 0.44f);
            Gl.glVertex2f(0.56f, 0.44f);
            Gl.glVertex2f(0.52f, 0.46f);

            Gl.glEnd();

            //ARCO
            float raio, x, y, pontos;
            raio = 0.1f;
            pontos = (2 * PI) / 1000;

            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glColor3f(1.0f, 0.0f, 0.0f);
            Gl.glVertex2f(0.5f, 0.6f);

            for (float angulo = 0; angulo <= 2 * PI; angulo += pontos)
            {
                x = (float)(raio * Math.Cos(angulo) + 0.5f);
                y = (float)(raio * Math.Sin(angulo) + 0.6f);
                Gl.glVertex2f(x, y);
            }
            Gl.glEnd();

            raio = 0.08f;
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glColor3f(1.0f, 1.0f, 1.0f);
            Gl.glVertex2f(0.5f, 0.6f);

            for (float angulo = 0; angulo <= 2 * PI; angulo += pontos)
            {
                x = (float)(raio * Math.Cos(angulo) + 0.5f);
                y = (float)(raio * Math.Sin(angulo) + 0.6f);
                Gl.glVertex2f(x, y);
            }
            Gl.glEnd();

            raio = 0.06f;
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glColor3f(1.0f, 0.0f, 0.0f);
            Gl.glVertex2f(0.5f, 0.6f);

            for (float angulo = 0; angulo <= 2 * PI; angulo += pontos)
            {
                x = (float)(raio * Math.Cos(angulo) + 0.5f);
                y = (float)(raio * Math.Sin(angulo) + 0.6f);
                Gl.glVertex2f(x, y);
            }
            Gl.glEnd();

            raio = 0.04f;
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glColor3f(1.0f, 1.0f, 1.0f);
            Gl.glVertex2f(0.5f, 0.6f);

            for (float angulo = 0; angulo <= 2 * PI; angulo += pontos)
            {
                x = (float)(raio * Math.Cos(angulo) + 0.5f);
                y = (float)(raio * Math.Sin(angulo) + 0.6f);
                Gl.glVertex2f(x, y);
            }
            Gl.glEnd();

            raio = 0.02f;
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glColor3f(1.0f, 0.0f, 0.0f);
            Gl.glVertex2f(0.5f, 0.6f);

            for (float angulo = 0; angulo <= 2 * PI; angulo += pontos)
            {
                x = (float)(raio * Math.Cos(angulo) + 0.5f);
                y = (float)(raio * Math.Sin(angulo) + 0.6f);
                Gl.glVertex2f(x, y);
            }
            Gl.glEnd();
        }

        static void Acerto()
        {
            Gl.glPushMatrix();
            Gl.glTranslatef(0.0f, 0.0f, 0.0f);
            Gl.glColor3f(0.0f, 1.0f, 0.0f);
            Gl.glLineWidth(5);
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glVertex2f(0.12f, 0.86f);
            Gl.glVertex2f(0.09f, 0.80f);
            Gl.glVertex2f(0.11f, 0.81f); Gl.glEnd();
            Gl.glBegin(Gl.GL_TRIANGLE_FAN);
            Gl.glVertex2f(0.11f, 0.81f);
            Gl.glVertex2f(0.11f, 0.825f);
            Gl.glVertex2f(0.21f, 0.85f);
            Gl.glEnd();
        }

        static void Erro()
        {
            Gl.glPushMatrix();
            Gl.glTranslatef(0.0f, 0.0f, 0.0f);
            Gl.glColor3f(1.0f, 0.0f, 0.0f);
            Gl.glLineWidth(30f);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex2f(0.14f, 0.86f);
            Gl.glVertex2f(0.09f, 0.80f);
            Gl.glEnd();
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex2f(0.09f, 0.86f);
            Gl.glVertex2f(0.14f, 0.80f);
            Gl.glEnd();
            Gl.glPopMatrix();
        }

        static void limiteMover()
        {
            if (tx < -0.5f) { tx = -0.5f; }
            if (tx >= 0.5f) { tx = 0.5f; }
            if (tm < -0.6f) { tm = -0.6f; }
            if (tm >= 0.1f) { tm = 0.1f; }
            Glut.glutPostRedisplay();
        }


        static void Mover(int key, int x, int y)
        {
            if (key == Glut.GLUT_KEY_LEFT) { tx -= 0.01f; }
            if (key == Glut.GLUT_KEY_RIGHT) { tx += 0.01f; }
            if (key == Glut.GLUT_KEY_UP) { tm += 0.01f; }
            if (key == Glut.GLUT_KEY_DOWN) { tm -= 0.01f; }
        }

        static void Enviar(string comando)
        {
            porta.Open();
            porta.Write(comando);
            porta.Close();
        }

        static void Teclado(byte key, int x, int y)
        {
            if (key == 48) { sinal = 0; }   //Tecla	0	deixa de noite
            if (key == 49) { sinal = 1; }   //Tecla	1	deixa de dia
            if (key == 13)
            {
                while (bit == true)
                {

                }
                if (Math.Sqrt((Math.Pow((0 - tx), 2) + Math.Pow((-0.27 - tm), 2))) < 0.1)
                {
                    Enviar("A");
                    bitAcerto = true;
                    Thread t = new Thread(BitAcerto);
                    t.Start();
                }
                else
                {
                    Enviar("E");
                    bitErro = true;
                    Thread t = new Thread(BitErro);
                    t.Start();
                }
            }
        }

        static void BitAcerto()
        {
            Thread.Sleep(1500);
            bitAcerto = false;
        }

        static void BitErro()
        {
            Thread.Sleep(1500);
            bitErro = false;
        }

        static void Timer(int value)
        {
            rot += 10f;
            // Redesenha o quadrado com as novas coordenadas 
            Glut.glutPostRedisplay();
            Glut.glutTimerFunc(100, Timer, 1);
        }
        static void TimerLuz(int value)
        {
            if (bit == false)
            {
                bit = true;
                //Recebe();
            }

            Glut.glutTimerFunc(1000, TimerLuz, 1);
        }

        //static void Recebe()
        //{
        //    String recebido = "";
        //    int aux;
        //    porta.Open();
        //    recebido = porta.ReadLine();
        //    porta.Close();
        //    bit = false;
        //    if ((recebido.Length > 4) || (recebido == "\r") || (recebido == "") || (recebido == "1\r0\r"))
        //    {
        //        aux = 0;
        //    }
        //    else
        //    {
        //        tem4 = tem3;
        //        tem3 = tem2;
        //        tem2 = tem1;
        //        tem1 = Convert.ToInt16(recebido);
        //        if ((tem1 + tem2 + tem3 + tem4) / 4 > 400)
        //        {
        //            sinal = 0;
        //        }
        //        else
        //        {
        //            sinal = 1;
        //        }
        //    }
        //    //Console.WriteLine("Luz: "+ (tem1 + tem2 + tem3 + tem4) / 4);
        //}

        static void desenha()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);//Limpa a janela de visualização com a cor de fundo especificada

            if (sinal == 0) { fundoDia(); }
            if (sinal == 1) { fundoNoite(); raiosLuz(); }

            alvo();
            atirador();
            limiteMover();
            //Erro();

            Glut.glutSwapBuffers();
        }

        static void inicializa()
        {
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Glu.gluOrtho2D(0.0, 1.0, 0.0, 1.0);
            Gl.glClearColor(1, 1, 1, 0);

        }

        static void Main()
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_SINGLE | Glut.GLUT_RGB);
            Glut.glutInitWindowSize(650, 650);
            Glut.glutInitWindowPosition(100, 100);
            Glut.glutCreateWindow("Tiro ao Alvo");
            Glut.glutDisplayFunc(desenha);
            Glut.glutSpecialFunc(Mover);
            Glut.glutKeyboardFunc(new Glut.KeyboardCallback(Teclado));  //Chama  as  funcoes  do teclado
            Glut.glutTimerFunc(100, Timer, 1);
            Glut.glutTimerFunc(1000, TimerLuz, 1);
            inicializa();
            Glut.glutMainLoop();
        }

    }
}

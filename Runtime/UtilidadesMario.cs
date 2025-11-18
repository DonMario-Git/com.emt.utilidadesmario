using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace EMT.UtilidadesMario
{
    public static class UtilidadesMario
    {
        #region Tiempo

        /// <summary>
        /// Calcula la edad en años entre dos fechas (fechaNacimiento y fechaActual).
        /// </summary>
        public static int CalcularEdad(DateTime fechaNacimiento, DateTime fechaActual)
        {
            int edad = fechaActual.Year - fechaNacimiento.Year;
            if (fechaActual < fechaNacimiento.AddYears(edad))
                edad--;
            return edad;
        }

        /// <summary>
        /// devuelve la fecha de hoy sin la hora
        /// </summary>
        /// <returns></returns>
        public static DateTime ObtenerFechaHoy()
        {
            return DateTime.Today;
        }

        /// <summary>
        /// suma dias a una fecha DateTime
        /// </summary>
        /// <param name="tiempo"></param>
        /// <param name="cantidadDiasSumar"></param>
        /// <returns></returns>
        public static DateTime SumarDias(DateTime tiempo, int cantidadDiasSumar)
        {
            return tiempo.AddDays(cantidadDiasSumar);
        }

        /// <summary>
        /// convierte una fecha DateTime a string con formato dd/MM/yy
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static string DateTimeToString(DateTime fecha)
        {
            return fecha.ToString("dd/MM/yy");
        }

        /// <summary>
        /// convierte un string con formato dd/MM/yy a DateTime
        /// </summary>
        /// <param name="fecha_dd_mm_aa"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        public static DateTime StringToDateTime(string fecha_dd_mm_aa)
        {
            try
            {
                return DateTime.ParseExact(fecha_dd_mm_aa, "dd/MM/yy", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                throw new FormatException($"El string '{fecha_dd_mm_aa}' no tiene el formato esperado dd/MM/yy.");
            }
        }

        #endregion

        #region Activar objetos

        public static void DesactivarObjeto(this GameObject obj)
        {
            obj.SetActive(false);
        }
        
        public static void DesactivarObjeto(this MonoBehaviour obj)
        {
            obj.gameObject.SetActive(false);
        }
        
        public static void DesactivarObjeto(this Renderer obj)
        {
            obj.gameObject.SetActive(false);
        }




        public static void ActivarObjeto(this GameObject obj)
        {
            obj.SetActive(true);
        }

        public static void ActivarObjeto(this MonoBehaviour obj)
        {
            obj.gameObject.SetActive(true);
        }

        public static void ActivarObjeto(this Renderer obj)
        {
            obj.gameObject.SetActive(true);
        }



        public static void DesactivarComponente(this MonoBehaviour obj)
        {
            obj.enabled = false;
        }

        public static void ActivarComponente(this MonoBehaviour obj)
        {
            obj.enabled = true;
        }

        public static void DesactivarComponente(this Renderer obj)
        {
            obj.enabled = false;
        }

        public static void ActivarComponente(this Renderer obj)
        {
            obj.enabled = true;
        }

        #endregion

        #region Listas

        public static void DesordenarLista<T>(this IList<T> list)
        {
            int n = list.Count;
            for (int i = 0; i < n; i++)
            {
                int r = Random.Range(i, n);
                (list[i], list[r]) = (list[r], list[i]);
            }
        }

        #endregion

        #region Cadenas de texto
        /// <summary>
        /// Remueve espacios en blanco al inicio y al final de un string.
        /// </summary>
        public static string TrimEdges(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            return text.Trim();
        }

        /// <summary>
        /// Verifica si un string empieza con un carácter específico.
        /// </summary>
        public static bool StartsWithChar(this string text, char character)
        {
            if (string.IsNullOrEmpty(text))
                return false;

            return text[0] == character;
        }

        /// <summary>
        /// Verifica si un string termina con un carácter específico.
        /// </summary>
        public static bool EndsWithChar(this string text, char character)
        {
            if (string.IsNullOrEmpty(text))
                return false;

            return text[^1] == character; // ^1 = último carácter
        }

        /// <summary>
        /// Remueve espacios dobles dentro del string, dejando solo espacios simples.
        /// </summary>
        public static string NormalizeInnerSpaces(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            text = text.Trim();
            while (text.Contains("  "))
            {
                text = text.Replace("  ", " ");
            }
            return text;
        }

        /// <summary>
        /// Remueve todos los espacios en blanco extra (espacios, tabulaciones, nuevas líneas) en el string, dejando solo espacios simples.
        /// </summary>
        public static string NormalizeWhitespace(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            text = text.Trim();
            text = Regex.Replace(text, @"\s+", " ");
            return text;
        }

        /// <summary>
        /// Valida que tan confiable es un Email
        /// </summary>
        public static bool EsUnEmailValido(this string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        #endregion

        #region Otros

        /// <summary>
        /// determina si un numero es par
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool EsPar(this int number)
        {
            return number % 2 == 0;
        }

        /// <summary>
        /// devuelve un Vector2 con valores aleatorios entre min y max
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static Vector2 Vector2RangeMagnitude(float min, float max)
        {
            return new Vector2(Random.Range(min, max), Random.Range(min, max));
        }

        /// <summary>
        /// devuelve un Vector3 con valores aleatorios entre min y max
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static Vector3 Vector3RangeMagnitude(float min, float max)
        {
            return new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));
        }

        /// <summary>
        /// Rota un transform en Y segun un bool: true = 180, false = 0   
        /// </summary>
        /// <param name="isFliped"></param>
        /// <param name="Tr"></param>
        public static void VoltearRotEnY(this Transform Tr, bool isFliped)
        {
            Tr.rotation = Quaternion.Euler(0, isFliped ? 180 : 0, 0);
        }

        /// <summary>
        /// devuelve un bool aleatorio segun una probabilidad de que sea true
        /// </summary>
        /// <param name="probabilidadTrue"></param>
        /// <returns></returns>
        public static bool RandomBool(int probabilidadTrue = 50)
        {
            int Rng = Random.Range(0, 101);
            return Rng < probabilidadTrue;
        }

        /// <summary>
        /// Determina si un numero esta entre un rango (incluye el minimo, excluye el maximo)
        /// </summary>
        /// <param name="num"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool EstaEntreRango(this float num, float min, float max)
        {
            return num >= min && num < max;
        }

        /// <summary>
        /// Determina si un numero esta entre un rango (incluye el minimo, excluye el maximo)
        /// </summary>
        /// <param name="num"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool EstaEntreRango(this int num, int min, int max)
        {
            return num >= min && num < max;
        }

        /// <summary>
        /// Rota un transform en angulo Z para que apunte a una posicion objetivo en 2D, con un angulo de offset|
        /// </summary>
        /// <param name="targetPosition"></param>
        /// <param name="transformToRotate"></param>
        /// <param name="offSetAngle"></param>
        /// <returns></returns>
        public static void RotarApuntar(this Transform transformToRotate, Vector3 targetPosition, float offSetAngle)
        {
            Vector3 direccion = targetPosition - transformToRotate.position;
            float AnguloZ = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

            transformToRotate.rotation = Quaternion.AngleAxis(AnguloZ + offSetAngle, Vector3.forward);
        }

        /// <summary>
        /// Obtiene el ID único del dispositivo
        /// </summary>
        public static string GetDeviceID()
        {
            return SystemInfo.deviceUniqueIdentifier;
        }    

        #endregion
    }
}
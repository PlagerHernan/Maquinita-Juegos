using System.Collections.Generic;
using UnityEngine;

public class LanguageExtractor
{
    //Devuelve un diccionario con todos los textos. El mismo esta compuesto de la siguiente manera:
    //  ID
    //  |
    //   --> Idioma [ingles, español, portugues, etc.]
    //       |
    //        --> Texto

    // Es decir, por cada ID hay diferentes idiomas, y por cada idioma hay un texto.
    public static Dictionary<string, Dictionary<Language, string>> ExtractTexts(string source, string sheet)
    {
        //Creamos una variable del tipo que vamos a tener que devolver
        var codex = new Dictionary<string, Dictionary<Language, string>>();

        //un contador de lineas para saber en donde fallo,si es que falla
        int lineNum = 0;

        //Cortamos los renglones cada vez que encontremos un salto de linea
        var rows = sheet.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        //Booleano para saber si es la primera linea
        bool first = true;

        //Diccionario para saber en que posicion esta determinada columna, Ej: <Idioma,0>, <Texto,1>
        var languagesToIndex = new Dictionary<int, Language>();

        foreach (var row in rows)
        {
            //Sumamos para saber que estamos en la primera linea
            lineNum++;
            //Separamos por columna al encontrar un ";"...tambien se toma la ","
            var cells = row.Split(',');

            #region Headers initialisation

            //Si es la primera linea 
            if (first)
            {
                first = false;//Ya sabemos que la siguiente no va a ser la primera

                for (int i = 1; i < cells.Length; i++)
                {
                    try
                    {
                        languagesToIndex[i] = (Language)System.Enum.Parse(typeof(Language), cells[i]); //Guardamos el indice de donde se encuentra esa columna que tiene un nombre,en nuestro caso ID,Lenguaje,Texto,etc
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(string.Format("Parsing CSV file {1} at header. Invalid language {0}", cells[i], source));
                        Debug.Log(e.ToString());
                    }
                }

                continue;
            }

            #endregion

            //------------- A PARTIR DE ACÁ LOS HEADERS YA ESTÁN GUARDADOS ---------------------

            //Si detectamos que hay una diferencia en las columnas avisamos en consola para que sepamos que algo falla en el documento
            if (cells.Length != languagesToIndex.Count + 1)
            {
                Debug.Log(string.Format("Parsing CSV file {2} at line {0} columns {1} should be {3}", lineNum, cells.Length, source, languagesToIndex.Count + 1));
                continue;
            }

            var id = cells[0];

            if (!codex.ContainsKey(id))
                codex[id] = new Dictionary<Language, string>();

            for (int i = 1; i < cells.Length; i++)
            {
                var lang = languagesToIndex[i];
                var text = cells[i];

                codex[id][lang] = text;
            }
        }
        return codex;
    }
}

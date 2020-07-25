using System.Collections.Generic;
using System.Text.RegularExpressions;
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

        //Diccionario para saber en que posicion esta determinada columna, Ej: <1, English>, <2, Spanish>
        var languagesToIndex = new Dictionary<int, Language>();

        //Creo una expresion regular que me permite separar los campos de una fila sin que me detecte las comas (las que son parte de textos)
        Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

        foreach (var row in rows)
        {
            //Sumamos para saber que estamos en la primera linea
            lineNum++;

            //Separamos cada celda con el parseador definido anteriormente.
            var cells = CSVParser.Split(row);

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
                //Quito los espacios y las comillas del inicio del texto
                cells[i] = cells[i].TrimStart(' ', '"');

                //Quito las comillas del final del texto
                cells[i] = cells[i].TrimEnd('"');

                var lang = languagesToIndex[i];
                var text = cells[i];

                codex[id][lang] = text;
            }
        }
        return codex;
    }
}

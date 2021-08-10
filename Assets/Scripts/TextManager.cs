using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public string TextFetcher(string text)
    {
        string a;

        switch (text)
        {
            case "Home":
                a = "Change tabs with the Left and Right arrow keys.\n\nScroll with Up and Down arrow keys (where possible).\n\nPress Enter to show text immediately.\n\nPress Esc to exit.";
                //a = "-> TITLE <-\naba\nsdasd\nasddf\nvzv\nxvc\n\nxcvxc\nxcvssd\n\nasddfg\nasdfd\nasdggrw\nssssssss\na\n" +
                //    "b\nsasdasd\n1\n2\n3\n4\n21. La primera linia fora de la pantalla\n22. La segona linia fora de la pantalla\n" +
                //    "23. La tercera linia fora de la pantalla";
                break;
            case "Education":
                a = "From 2005 until 2020 I studied in Aula Escola Europea, in Barcelona. The last two years (2018-2020) I was awarded the CiMs+CELLEX scholarship that paid full tuition " + 
                    "to course International Baccalaureate and the Spanish Baccalaureate (Bachillerato). The scholarship also paid a summer internship, that I did in the ICN2 " +
                    "(Catalan Institute of Nanoscience and Nanotechnology).\n\nAfterwards I started studying in the University of Sheffield Materials Science & Engineering.";
                break;
            case "Profile":
                a = "NAME: Pedro Juan Royo.\n\nBIRTHDAY: 19/03/2002.\n\nOCCUPATION: Full time student at University of Sheffield, studying Materials Science & Engineering.\n\n" +
                    "LANGUAGES: Catalan (Native), Spanish (Native), English, French.\n\nAWARDS:\n- HP Codewars. 1st place in Sant Cugat del Vallès, Barcelona, and 12th place in Houston, Texas.\n" +
                    "- The Joe Hemmant Award. An Alumni of the Department of Materials Science & Engineering in the University of Sheffield gives this award to two first year students on the basis of " + 
                    "entry grades and continued performance.\n- Sir Harold West Award. Given by the University of Sheffield due to personal and academic performance during the first year of the Materials " +
                    "Science & Engineering course.\n\n";
                break;
            case "Hobbies":
                a = "I like coding stuff for fun (I have done things in C++, C#, Python, MATLAB, Scratch), reading manga and watching anime, and cooking.\n";
                break;
            case "Social":
                a = "| Contact me |\n -> pedro.juan.royo@gmail.com\n\n| Social accounts |\n -> Reddit: u/Parzival1918\n -> Instagram: @Parzival1918\n -> Twitter: @Parzival1918" + 
                    "\n -> LinkedIn: Pedro Juan Royo";
                break;

            default:
                a = "Could not find related text. Try another section.";
                break;
        }

        return a;
    }

    ///20 linies per pantalla
    public int MaxScroll(string text)
    {
        int a;

        switch (text)
        {
            case "Home":
                a = 0;
                break;
            case "Education":
                a = 0;
                break;
            case "Profile":
                a = 0;
                break;
            case "Hobbies":
                a = 0;
                break;
            case "Social":
                a = 0;
                break;

            default:
                a = 0;
                break;
        }

        return a;
    }
}

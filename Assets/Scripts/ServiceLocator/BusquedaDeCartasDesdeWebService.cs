﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class BusquedaDeCartasDesdeWebService : IBuscadorDeCartasGuardadas, IBuscadorDeTextosParaRuleta, IBuscadorDeTextosPorJuego
{
    
    public List<ICarta> BuscarCartasPorGenero(IGenero genero)
    {
        return Buscar(genero, "yoNuncaNunca");
    }

    public List<ICarta> BuscarTextosPorGenero(IGenero genero)
    {
        return Buscar(genero, "ruleta");
    }

    public List<ICarta> Buscar(IGenero genero, string juego)
    {
        List<ICarta> listaCartas = new List<ICarta>();

        ListaDeYoNuncaNunca lista = FactoriaDeListasDeCartas(juego);
        foreach (YoNuncaNunca yoNunca in lista.generos)
        {
            if (genero.Nombre == yoNunca.nombre)
            {
                foreach (TextoDeCarta textoDeCarta in yoNunca.listaTextoCarta)
                {
                    listaCartas.Add(new Carta(genero, textoDeCarta.value));
                }
            }
        }

        return listaCartas;
    }
    private Dictionary<string,ListaDeYoNuncaNunca> bodega;
    private ListaDeYoNuncaNunca FactoriaDeListasDeCartas(string cartaJuego)
    {
        bodega.TryGetValue(cartaJuego,  out var valor);
        return valor;
    }

    public BusquedaDeCartasDesdeWebService(string jsonDeCartasNuncaNunca,string jsonDeTextosParaRuleta,string jsonDeTextosParaPictonary)
    {
        Debug.Log(jsonDeCartasNuncaNunca);
        Debug.Log(jsonDeTextosParaRuleta);
        Debug.Log(jsonDeTextosParaPictonary);
        bodega = new Dictionary<string, ListaDeYoNuncaNunca>
        {
            {
                "ruleta",
                new ListaDeYoNuncaNunca
                {
                    generos = JsonUtility.FromJson<ListaDeYoNuncaNunca>(jsonDeTextosParaRuleta).generos
                }
            },
            {
                "yoNuncaNunca",
                new ListaDeYoNuncaNunca
                {
                    generos = JsonUtility.FromJson<ListaDeYoNuncaNunca>(jsonDeCartasNuncaNunca).generos
                }
            },
            {
                "pictonary",
                new ListaDeYoNuncaNunca
                {
                    generos = JsonUtility.FromJson<ListaDeYoNuncaNunca>(jsonDeTextosParaPictonary).generos
                }
            }
        };
    }

    /*Formato de carta en su minima exprecion
{
   "generos": [
       {
           "nombre": "Normal",--->este es el genero
           "listaTextoCarta": [
               {
               "value": "maté a nadie."--->Esta es la carta
               }
           ]
       }
   ]
}
*/
    private const string jsonDeTextosParaRuleta = "{\"generos\":[{\"nombre\":\"Normal\",\"listaTextoCarta\":[{\"value\":\"Tomás un trago\"},{\"value\":\"Un beso a la persona que tenés en frente\"},{\"value\":\"Repartis dos tragos\"},{\"value\":\"Volvés a girar\"},{\"value\":\"Volvés a girar\"},{\"value\":\"Tomás tres tragos\"},{\"value\":\"Tenés que decir un 'Yo Nunca'\"},{\"value\":\"Repartís 6 tragos\"},{\"value\":\"Te desafian a 'Verdad o Reto'\"},{\"value\":\"Fondo blanco o volvé a girar\"},{\"value\":\"Todos toman\"},{\"value\":\"Toman las mujeres\"},{\"value\":\"Toman los hombres\"},{\"value\":\"Toma el de tu derecha\"},{\"value\":\"Toma el de tu izquierda\"},{\"value\":\"Regalas un fondo blanco\"}]}]}";
    //private const string jsonDeCartasNuncaNunca = "{\"generos\":[{\"nombre\":\"Picante\",\"listaTextoCarta\":[{\"value\":\" hice un trío.\"},{\"value\":\" estuve en una orgía.\"},{\"value\":\" me sentí atraíd@ con alguien de los presentes.\"},{\"value\":\"A mi nunca me encontraron masturbandome.\"},{\"value\":\"A mi nunca me encontraron teniendo sexo.\"},{\"value\":\" encontré a alguien masturbandose.\"},{\"value\":\" encontré a alguien teniendo sexo.\"},{\"value\":\" tuve sexo en un lugar publico.\"},{\"value\":\" me masturbé en un lugar público.\"},{\"value\":\" tuve sexo en el mar.\"},{\"value\":\" tuve sexo en la playa.\"},{\"value\":\" tuve sexo en un probador. \"},{\"value\":\" me masturbé pensando en alguien de los presentes.\"},{\"value\":\" tuve un crush con un profesor/profesora.\"},{\"value\":\" estuve con una persona 5 años mayor.\"},{\"value\":\" estuve con una persona 10 años mayor.\"},{\"value\":\" estuve con una persona 5 años menor.\"},{\"value\":\" estuve con la pareja de un/a amig@.\"},{\"value\":\" estuve con la ex-pareja de un/a amig@.\"},{\"value\":\" tuve sexo en mi lugar de trabajo.\"},{\"value\":\"A mi nunca me gustó un/una compañer@ de trabajo.\"},{\"value\":\"A mi nunca me gustó un/una compañer@ de la escuela/universidad.\"},{\"value\":\" me sentí atraído por un familiar. (Que incomodo ser el unico tomando)\"},{\"value\":\" mandé nudes.\"},{\"value\":\" recibí nudes.\"},{\"value\":\" tuve sexo. (había que tantear)\"},{\"value\":\" hice sexting. (No vale googlear que es)\"},{\"value\":\" me grabé teniendo sexo.\"},{\"value\":\" tuve sexo con alguien que acababa de conocer. (hoy puede ser ese día)\"},{\"value\":\" fingí un orgasmo. \"},{\"value\":\" estuve sexsualmente con alguno de los presentes.\"},{\"value\":\" tuve sexo más de tres veces en un día.\"},{\"value\":\" estuve con más de dos personas en un mismo día. (Easy)\"},{\"value\":\" estuve con más de tres personas en un mismo día. (Normal)\"},{\"value\":\" estuve con más de cuatro personas en un mismo día. (Hardcore)\"},{\"value\":\" usé juguetes sexuales. \"},{\"value\":\" tuve una relación abierta.\"},{\"value\":\" hice un baile erotico.\"},{\"value\":\" recibí un baile erotico.\"},{\"value\":\" tomé la pastilla del día después. (PD: todos los hombres toman)\"},{\"value\":\" mandé un mensaje “hot” a alguien que no era. (Puede pasar)\"},{\"value\":\" estuve con una persona con pareja.\"},{\"value\":\" estuve con una persona casada.\"},{\"value\":\" me hice un test de embarazo. (PD: todos los hombres toman)\"},{\"value\":\" vi a mis padres teniendo sexo.\"},{\"value\":\" me masturbé mas de 3 veces en un día. (Es tu oportunidad de tomar!)\"},{\"value\":\" usé lubricante sexual.\"},{\"value\":\" usé preservativos de sabores.\"},{\"value\":\" usé preservativos luminosos. (Un mundo de experiencias que las lesbianas no conocen.)\"},{\"value\":\" recibí un cumplido por mis genitales. \"},{\"value\":\" rompí la cama teniendo sexo.\"},{\"value\":\" hice juego de rol durante el sexo.\"},{\"value\":\" dije el nombre de otra persona durante el sexo. (Que mala leche)\"},{\"value\":\" hice chanchadas en el cine.\"},{\"value\":\" hice cosas indecentes habiendo amigos cerca.\"},{\"value\":\" estuve con alguien sin saber su nombre.\"},{\"value\":\" miré porno.\"},{\"value\":\" miré porno con alguien.\"},{\"value\":\" hice streaptease.\"},{\"value\":\" recibí un streaptease.\"},{\"value\":\" tuve sexo bondage/sado. \"},{\"value\":\" tuve sexo en cama de mis padres. (Sin ellos, esperamos.)\"},{\"value\":\" tuve un fetiche extraño. (Cuenten que acá nadie juzga.)\"},{\"value\":\" tuve sexo en la cocina.\"},{\"value\":\" llegue al orgasmo con solo sexo oral.\"},{\"value\":\"tuve una erección en público. (PD: las mujeres toman)\"}]} , {\"nombre\":\"Normal\",\"listaTextoCarta\":[{\"value\":\"maté a nadie.\"},{\"value\":\"intenté mover cosas con la mente.\"},{\"value\":\"pretendí cantar sin saber la letra.\"},{\"value\":\"viaje fuera del país\"},{\"value\":\"di una indicación (dirección) falsa en la calle.\"},{\"value\":\"le pregunté algo a alguien que no trabajaba ahí.\"},{\"value\":\"mentí en el yo nunca.\"},{\"value\":\"jugué al yo nunca\"},{\"value\":\"bebí más de la cuenta. (Borrachera)\"},{\"value\":\"perdí el conocimiento por tomar. (Que se te apague el tele decimos)\"},{\"value\":\"robé nada de un almacén. (Puede ser un caramelo)\"},{\"value\":\" perdí mi documento.\"},{\"value\":\"perdí mi celular.\"},{\"value\":\"perdí mis llaves.\"},{\"value\":\"le dije mamá a una profesora.\"},{\"value\":\"me imaginé ser del sexo opuesto.\"},{\"value\":\"fui detenido por la policía\"},{\"value\":\"tuve que cortar la fiesta por denuncias de vecinos.\"},{\"value\":\"mentí en una entrevista de trabajo.\"},{\"value\":\"rendí un examen sin estudiar.\"},{\"value\":\"fuí borracho a la escuela/universidad.\"},{\"value\":\"convencí a un amigo de ir de fiesta aunque tenía que estudiar.\"},{\"value\":\"estuve más de tres días sin bañarme.\"},{\"value\":\"stalkee a uno de los presentes.\"},{\"value\":\"inventé una excusa para cancelar una cita/juntada/reunión.\"},{\"value\":\"pretendí una llamada para irme de algún lugar.\"},{\"value\":\"he meado en la pileta.\"},{\"value\":\" dije falsos spoilers solo para hacer enojar.\"},{\"value\":\"dije spoilers.\"},{\"value\":\" intenté volver con mi ex por la borrachera.\"},{\"value\":\"miré una serie solo porque los actores estaban buenos.\"},{\"value\":\" he meado en la calle.\"},{\"value\":\"estuve esposado.\"},{\"value\":\" llegué borracho a casa de mis padres.\"},{\"value\":\"fui descubierto llegando borracho a casa de mis padres.\"},{\"value\":\"fingí estar enfermo para no ir a trabajar/escuela.\"},{\"value\":\"hablé con amigos mientras hacía del número dos. (una cacona)\"},{\"value\":\" probé comida para animales. (Why not?)\"},{\"value\":\"le escribí borrach@ a mi ex.\"},{\"value\":\" stalkee a la nueva pareja de mi ex.\"},{\"value\":\" robé bebidas en un pub/boliche.\"},{\"value\":\" robé vasos en un pub/boliche.\"},{\"value\":\" hablé mal de alguno de los presentes.\"},{\"value\":\" fui infiel.\"},{\"value\":\" vomité en público por beber.\"},{\"value\":\" me tropecé por ir mirando el celular.\"},{\"value\":\" fui a trabajar borracho.\"},{\"value\":\" me emborraché en el trabajo.\"},{\"value\":\" me paseé desnudo por mi casa. (Puntos extra si había gente)\"},{\"value\":\" canté en la ducha. \"},{\"value\":\" me vestí con ropa del sexo opuesto.\"},{\"value\":\" vi a mis padres desnudos.\"},{\"value\":\" me emborraché por la mañana.\"},{\"value\":\" lloré por una película/serie.\"},{\"value\":\" bese a alguien sin saber su nombre.\"},{\"value\":\" jugué al streap poker.\"},{\"value\":\" me corté el pelo y me arrepentí.\"},{\"value\":\" chapé con más de dos personas el mismo día.\"},{\"value\":\" estuve preso.\"},{\"value\":\" recibí una multa.\"},{\"value\":\" manejé sin carnet o licencia.\"},{\"value\":\" recibí una paliza (me cagaron a trompadas)\"},{\"value\":\" sostuve un bebe.\"},{\"value\":\" choqué un auto.\"},{\"value\":\" choqué una bici/moto.\"},{\"value\":\" me caí de una bici/moto.\"},{\"value\":\" estuve en una clase online.\"},{\"value\":\" fui a ver a mi equipo en vivo.\"},{\"value\":\" fui a la cancha de mi equipo.\"},{\"value\":\" me escapé de casa.\"},{\"value\":\" me escapé de la escuela.\"},{\"value\":\" estuve desaparecido.\"},{\"value\":\"tome mate\"},{\"value\":\"tome terere\"},{\"value\":\"fumé marihuana\"},{\"value\":\"tomé falopa (inhalé cocaina)\"},{\"value\":\"colé pepa (LSD)\"},{\"value\":\"comí hongos\"},{\"value\":\"fui drogado al trabajo\"},{\"value\":\"fui drogado a la universidad/escuela\"}]} ]}";
    //private const string jsonDeTextosParaPictonary = "{\"generos\":[{\"nombre\":\"Normal\",\"listaTextoCarta\":[{\"value\":\"Plato de cereales\"},{\"value\":\"Hechicero/mago\"},{\"value\":\"Timbal\"},{\"value\":\"Rosa\"},{\"value\":\"Petalo de Rosa\"},{\"value\":\"Acostarse\"},{\"value\":\"Laberinto\"},{\"value\":\"Abofetear\"},{\"value\":\"Equipaje/bolsos\"},{\"value\":\"Trapecista\"},{\"value\":\"España\"},{\"value\":\"Cerveza\"},{\"value\":\"Vino\"},{\"value\":\"Medico\"},{\"value\":\"Cucharón\"},{\"value\":\"Bizcocho\"},{\"value\":\"Pez fuera del Agua\"},{\"value\":\"Tercer ojo\"},{\"value\":\"Calentar\"},{\"value\":\"Envase\"},{\"value\":\"Polvora\"},{\"value\":\"Pintar\"},{\"value\":\"Lagartija\"},{\"value\":\"Espina\"},{\"value\":\"Manifestacion\"},{\"value\":\"Guerra fría.\"},{\"value\":\"Fiesta\"},{\"value\":\"Amasar\"},{\"value\":\"Campana\"},{\"value\":\"Toldo\"},{\"value\":\"Centro\"},{\"value\":\"Amnesia\"},{\"value\":\"Cabalgar\"},{\"value\":\"Espadachín\"},{\"value\":\"Aluminio\"},{\"value\":\"Sangre\"},{\"value\":\"Estrecho\"},{\"value\":\"Cucharita\"},{\"value\":\"Rellenar\"},{\"value\":\"Remolino\"},{\"value\":\"Apagón\"},{\"value\":\"Estrella de mar\"},{\"value\":\"Billete falso\"},{\"value\":\"Embarazada\"},{\"value\":\"Muela de juicio\"},{\"value\":\"Mujer barbuda\"},{\"value\":\"Luciernaga\"},{\"value\":\"Lluvia\"},{\"value\":\"Tormenta\"},{\"value\":\"TV\"},{\"value\":\"Aerobico\"},{\"value\":\"Taxista\"},{\"value\":\"Carpeta\"},{\"value\":\"Kilo\"},{\"value\":\"Playa\"},{\"value\":\"Zancos\"},{\"value\":\"Experimentar\"},{\"value\":\"Feo\"},{\"value\":\"Mapa\"},{\"value\":\"Pescar\"},{\"value\":\"Mar\"},{\"value\":\"Tinta\"},{\"value\":\"Bomba\"},{\"value\":\"Delantal\"},{\"value\":\"Golpe bajo\"},{\"value\":\"Ajustar\"},{\"value\":\"India\"},{\"value\":\"Argentina\"},{\"value\":\"Buenos Aires\"},{\"value\":\"Segundero/cronometro\"},{\"value\":\"Suspender\"},{\"value\":\"Herramienta\"},{\"value\":\"Masa\"},{\"value\":\"Saltar/brincar\"},{\"value\":\"Agua mineral\"},{\"value\":\"Trineo\"},{\"value\":\"Memorizar\"},{\"value\":\"Kamikaze\"},{\"value\":\"Nudo\"},{\"value\":\"Tortugas ninjas\"},{\"value\":\"Mezclar\"},{\"value\":\"New York\"},{\"value\":\"Sarampión\"},{\"value\":\"Sombrero de fiesta\"},{\"value\":\"Diez en punto\"},{\"value\":\"Enfundar\"},{\"value\":\"Hombre bestia\"},{\"value\":\"Jeringa\"},{\"value\":\"Naufrago\"},{\"value\":\"Manco\"},{\"value\":\"Fundir\"},{\"value\":\"Soltero\"},{\"value\":\"Salsa picante\"},{\"value\":\"Cabeza\"},{\"value\":\"Hielo\"},{\"value\":\"Helio\"},{\"value\":\"Alzar/levantar\"},{\"value\":\"Cuerno\"},{\"value\":\"Oficina\"},{\"value\":\"Derrumbe\"},{\"value\":\"Estimular\"},{\"value\":\"Saxofon\"},{\"value\":\"Registradora/caja registradora\"},{\"value\":\"Bart Simpson\"},{\"value\":\"Triciclo\"},{\"value\":\"Vagón\"},{\"value\":\"Cartél de alto\"},{\"value\":\"Grafico\"},{\"value\":\"Media hora\"},{\"value\":\"Caballero\"},{\"value\":\"Carretera/autopista\"},{\"value\":\"Presidente\"},{\"value\":\"Paralelo\"},{\"value\":\"Parque de diversiones\"},{\"value\":\"Extender\"},{\"value\":\"Mujer maravilla\"},{\"value\":\"Vestido de fiesta\"},{\"value\":\"Punto y coma\"},{\"value\":\"Si\"},{\"value\":\"No\"},{\"value\":\"Ebanista\"},{\"value\":\"Ladrillo\"},{\"value\":\"Anillos de humo\"},{\"value\":\"Censura\"},{\"value\":\"Puente colgante\"},{\"value\":\"Salida de emergencia\"},{\"value\":\"Pato de goma\"},{\"value\":\"Museo\"},{\"value\":\"Avión\"},{\"value\":\"Compactador\"},{\"value\":\"Arco de triunfo\"},{\"value\":\"Pato lucas\"},{\"value\":\"Chocolate\"},{\"value\":\"Café\"},{\"value\":\"Bongo\"},{\"value\":\"Baño de hombres\"},{\"value\":\"Pintar\"},{\"value\":\"Negativo\"},{\"value\":\"Miel\"},{\"value\":\"Dormir\"},{\"value\":\"Drogas\"},{\"value\":\"Tornado\"},{\"value\":\"Esculpir\"},{\"value\":\"Rueda de carro\"},{\"value\":\"Tobillo\"},{\"value\":\"Motocicleta\"},{\"value\":\"Atardecer\"},{\"value\":\"Michael Jackson\"},{\"value\":\"Mesa de pool\"},{\"value\":\"Cortar leña\"},{\"value\":\"Escalones\"},{\"value\":\"Cielo\"},{\"value\":\"Trebol de la suerte\"},{\"value\":\"Hotel\"},{\"value\":\"Codigo de área\"},{\"value\":\"Fumar\"},{\"value\":\"Fragil\"},{\"value\":\"Kilometro\"},{\"value\":\"Carpa\"},{\"value\":\"Pastillas\"},{\"value\":\"Comer\"},{\"value\":\"Elastico\"},{\"value\":\"Zapatos rotos\"},{\"value\":\"Maratón\"},{\"value\":\"Zanahoria\"},{\"value\":\"Lavarropas\"},{\"value\":\"Champagne\"},{\"value\":\"Rompecabezas\"},{\"value\":\"Peluquero\"},{\"value\":\"Caballo de troya\"},{\"value\":\"Ventana rota\"},{\"value\":\"Edad media\"},{\"value\":\"Locura\"},{\"value\":\"Luchar\"},{\"value\":\"Toro\"},{\"value\":\"Lanzallamas\"},{\"value\":\"Auto deportivo\"},{\"value\":\"Magia\"},{\"value\":\"Diablo\"},{\"value\":\"Té\"},{\"value\":\"Entrenar\"},{\"value\":\"Tercer mundo\"},{\"value\":\"Compartir\"},{\"value\":\"Pianista\"},{\"value\":\"Reloj de sol\"},{\"value\":\"Hija\"},{\"value\":\"Trompa\"},{\"value\":\"Ropero\"},{\"value\":\"Golpear\"},{\"value\":\"Cinturón\"},{\"value\":\"Evolución\"},{\"value\":\"Ondular\"},{\"value\":\"Albondiga\"},{\"value\":\"Chimenea\"},{\"value\":\"Remo\"},{\"value\":\"Gobierno\"},{\"value\":\"Iglú\"},{\"value\":\"Niño jugando\"},{\"value\":\"Borracho\"},{\"value\":\"Corona\"},{\"value\":\"Cueva\"},{\"value\":\"Maradona\"}]}]}";
}
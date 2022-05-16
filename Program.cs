using System.Text.Json;

Int64 _totalOuvidoMilissegundos;
float _totalOuvidoMinutos;
float _totalOuvidoHoras;
float _totalOuvidoDias;
string _linha;

string fileName = "StreamingHistory0.json";
string jsonString = File.ReadAllText(fileName);
var _listaArquivoJson = JsonSerializer.Deserialize<List<StreamMusic>>(jsonString);

Console.WriteLine($"There are {_listaArquivoJson.Count} music");

_totalOuvidoMilissegundos  = _listaArquivoJson.Sum(x => x.msPlayed);

var _listaTotalMensalArtistaTemporaria = _listaArquivoJson
  .Select(c => new{ 
    artistName = c.artistName,
    trackName = c.trackName, 
    mes = c.endTime.Substring(5,2),
    dia = c.endTime.Substring(8,2),
    msPlayed = c.msPlayed,
    vez = 1})
    .ToList();

var _listaTotaisMusica = _listaTotalMensalArtistaTemporaria.GroupBy(x => new{x.artistName, x.trackName})
  .Select(c => new{
    artistName = c.Key.artistName,
    trackName = c.Key.trackName,
    totalVezesExecutadas = c.Sum(y => y.vez), 
    msPlayedTotal = c.Sum(y => y.msPlayed)})
    .OrderByDescending(o => o.totalVezesExecutadas);

var _listaTotalArtista = _listaTotalMensalArtistaTemporaria.GroupBy(x => new{x.artistName})
  .Select(c => new{
    artistName = c.Key.artistName,
    totalVezesExecutadas = c.Sum(y => y.vez),
    msPlayedTotal = c.Sum(y => y.msPlayed)})
    .OrderByDescending(o => o.msPlayedTotal);

var _listaTotaisMensais = _listaTotalMensalArtistaTemporaria.GroupBy(x => x.mes)
  .Select(c => new{
    mes = c.Key,
    totalVezesExecutadas = c.Sum(y => y.vez),
    msPlayedTotal = c.Sum(y => y.msPlayed)})
    .OrderByDescending(o => o.msPlayedTotal);

var _listaTotalMensalArtista = _listaTotalMensalArtistaTemporaria.GroupBy(x => new{x.artistName, x.mes})
  .Select(c => new{
    artistName = c.Key.artistName,
    mes = c.Key.mes,
    totalTempoExecutado = c.Sum(y => y.msPlayed),
    vezesExecutadas = c.Sum(y => y.vez)
    })
    .OrderByDescending(o => o.vezesExecutadas)
    .OrderByDescending(o => o.mes)
    .ToList();

var _listaTotalDiariaMusica = _listaTotalMensalArtistaTemporaria.GroupBy(x => new{x.mes,x.dia})
  .Select(c => new{
    mes = c.Key.mes,
    dia = c.Key.dia,
    totalTempoExecutado = c.Sum(y => y.msPlayed),
    vezesExecutadas = c.Sum(y => y.vez)
    })
    .OrderByDescending(o => o.vezesExecutadas)
    .OrderByDescending(o => o.dia)
    .OrderByDescending(o => o.mes)
    .ToList();    

var _listaTotalDiariaArtistaMusica = _listaTotalMensalArtistaTemporaria.GroupBy(x => new{x.artistName, x.trackName,x.mes,x.dia})
  .Select(c => new{
    artistName = c.Key.artistName,
    trackName = c.Key.trackName,
    mes = c.Key.mes,
    dia = c.Key.dia,
    totalTempoExecutado = c.Sum(y => y.msPlayed),
    vezesExecutadas = c.Sum(y => y.vez)
    })
    .OrderByDescending(o => o.vezesExecutadas)
    .OrderByDescending(o => o.dia)
    .OrderByDescending(o => o.mes)
    .ToList();

foreach( var item in _listaTotalArtista) //\r\n
{
  _totalOuvidoMinutos = item.msPlayedTotal / 60000;
  _totalOuvidoHoras = _totalOuvidoMinutos / 60;
  _totalOuvidoDias = _totalOuvidoHoras / 24;

  _linha = "Artista = " + item.artistName +
    "\r\nVezes Executadas = "+ item.totalVezesExecutadas.ToString() +
    "\r\nTempo Total Execução Milissegundos = "+ item.msPlayedTotal.ToString() + 
    "\r\nTempo Total Execução Minutos = "+ _totalOuvidoMinutos.ToString() + 
    "\r\nTempo Total Execução Horas = "+ _totalOuvidoHoras.ToString() + 
    "\r\nTempo Total Execução Dias = "+ _totalOuvidoDias.ToString() + "\r\n\r\n";

  File.AppendAllText("ResultadoAnaliseArtista.txt", _linha);

}

foreach( var item in _listaTotaisMusica) //\r\n
{
  _totalOuvidoMinutos = item.msPlayedTotal / 60000;
  _totalOuvidoHoras = _totalOuvidoMinutos / 60;
  _totalOuvidoDias = _totalOuvidoHoras / 24;

  _linha = "Artista = " + item.artistName +
    "\r\nMusica = "+ item.trackName +
    "\r\nVezes Executadas = "+ item.totalVezesExecutadas.ToString() +
    "\r\nTempo Total Execução Milissegundos = "+ item.msPlayedTotal.ToString() + 
    "\r\nTempo Total Execução Minutos = "+ _totalOuvidoMinutos.ToString() + 
    "\r\nTempo Total Execução Horas = "+ _totalOuvidoHoras.ToString() + 
    "\r\nTempo Total Execução Dias = "+ _totalOuvidoDias.ToString() + "\r\n\r\n";

  File.AppendAllText("ResultadoAnaliseMusica.txt", _linha);

}

foreach( var item in _listaTotaisMensais) //\r\n
{
  _totalOuvidoMinutos = item.msPlayedTotal / 60000;
  _totalOuvidoHoras = _totalOuvidoMinutos / 60;
  _totalOuvidoDias = _totalOuvidoHoras / 24;

  _linha = "Mês = " + item.mes +
    "\r\nVezes Executadas = "+ item.totalVezesExecutadas.ToString() +
    "\r\nTempo Total Execução Milissegundos = "+ item.msPlayedTotal.ToString() + 
    "\r\nTempo Total Execução Minutos = "+ _totalOuvidoMinutos.ToString() + 
    "\r\nTempo Total Execução Horas = "+ _totalOuvidoHoras.ToString() + 
    "\r\nTempo Total Execução Dias = "+ _totalOuvidoDias.ToString() + "\r\n\r\n";

  File.AppendAllText("ResultadoAnaliseMusicaMensalTempo.txt", _linha);

}

foreach( var item in _listaTotalMensalArtista) //\r\n
{
  _totalOuvidoMinutos = item.totalTempoExecutado / 60000;
  _totalOuvidoHoras = _totalOuvidoMinutos / 60;
  _totalOuvidoDias = _totalOuvidoHoras / 24;

  _linha = "Artista = " + item.artistName +
    "\r\nMês = "+ item.mes +
    "\r\nVezes Executadas = "+ item.vezesExecutadas.ToString() +
    "\r\nTempo Total Execução Milissegundos = "+ item.totalTempoExecutado.ToString() + 
    "\r\nTempo Total Execução Minutos = "+ _totalOuvidoMinutos.ToString() + 
    "\r\nTempo Total Execução Horas = "+ _totalOuvidoHoras.ToString() + 
    "\r\nTempo Total Execução Dias = "+ _totalOuvidoDias.ToString() + "\r\n\r\n";

  File.AppendAllText("ResultadoAnaliseArtistaMessal.txt", _linha);

}

foreach( var item in _listaTotalDiariaArtistaMusica) //\r\n
{
  _totalOuvidoMinutos = item.totalTempoExecutado / 60000;
  _totalOuvidoHoras = _totalOuvidoMinutos / 60;
  _totalOuvidoDias = _totalOuvidoHoras / 24;

  _linha = "Artista = " + item.artistName +
    "\r\nMusica = "+ item.trackName +
    "\r\nMês = "+ item.mes +
    "\r\nDia = "+ item.dia +
    "\r\nVezes Executadas = "+ item.vezesExecutadas.ToString() +
    "\r\nTempo Total Execução Milissegundos = "+ item.totalTempoExecutado.ToString() + 
    "\r\nTempo Total Execução Minutos = "+ _totalOuvidoMinutos.ToString() + 
    "\r\nTempo Total Execução Horas = "+ _totalOuvidoHoras.ToString() + 
    "\r\nTempo Total Execução Dias = "+ _totalOuvidoDias.ToString() + "\r\n\r\n";

  File.AppendAllText("ResultadoAnaliseDiariaArtistaMusica.txt", _linha);

}

foreach( var item in _listaTotalDiariaMusica) //\r\n
{
  _totalOuvidoMinutos = item.totalTempoExecutado / 60000;
  _totalOuvidoHoras = _totalOuvidoMinutos / 60;
  _totalOuvidoDias = _totalOuvidoHoras / 24;

  _linha = "Mês = "+ item.mes +
    "\r\nDia = "+ item.dia +
    "\r\nVezes Executadas = "+ item.vezesExecutadas.ToString() +
    "\r\nTempo Total Execução Milissegundos = "+ item.totalTempoExecutado.ToString() + 
    "\r\nTempo Total Execução Minutos = "+ _totalOuvidoMinutos.ToString() + 
    "\r\nTempo Total Execução Horas = "+ _totalOuvidoHoras.ToString() + 
    "\r\nTempo Total Execução Dias = "+ _totalOuvidoDias.ToString() + "\r\n\r\n";

  File.AppendAllText("ResultadoAnaliseDiariaAMusica.txt", _linha);

}


System.Console.WriteLine($"Total  de milissegundos {_totalOuvidoMilissegundos}");

_totalOuvidoMilissegundos = _totalOuvidoMilissegundos / 60000;

System.Console.WriteLine($"Total de minutos {_totalOuvidoMilissegundos}");

_totalOuvidoMilissegundos = _totalOuvidoMilissegundos / 60;

System.Console.WriteLine($"Total de horas {_totalOuvidoMilissegundos}");

_totalOuvidoMilissegundos = _totalOuvidoMilissegundos / 24;

System.Console.WriteLine($"Total de dias {_totalOuvidoMilissegundos}");
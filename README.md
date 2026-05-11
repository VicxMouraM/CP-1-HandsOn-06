# ⚡ AsyncLab

## 🧪 Laboratório Async

### 🎯 Objetivo

Analisar o programa e tornar a sua execução assíncrona.

---

## 👥 Membros do grupo

- Victoria Moura - RM 555474

---

## 🛠️ Descrição das modificações realizadas

O programa original foi alterado para utilizar chamadas assíncronas em operações de entrada e saída, além de paralelismo em trechos de processamento mais pesado.

As principais modificações foram:

- Uso de `HttpClient.GetStringAsync` para baixar o CSV de municípios de forma assíncrona;
- Uso de `File.WriteAllTextAsync` para salvar o arquivo CSV temporário;
- Uso de `File.ReadAllLinesAsync` para leitura assíncrona do CSV;
- Uso de `Task.WhenAll` para processar as UFs de forma concorrente;
- Uso de `File.WriteAllLinesAsync` para gerar os arquivos `.csv`;
- Uso de `File.WriteAllTextAsync` para gerar os arquivos `.json`;
- Uso de `Parallel.For` para calcular os hashes dos municípios em paralelo;
- Medição do tempo total e do tempo por UF com `Stopwatch`.

---

## 📊 Impactos observados no tempo de execução

Com as alterações, o programa passou a evitar bloqueios em operações de download, leitura e escrita de arquivos.

O processamento das UFs passou a ocorrer de forma concorrente com `Task.WhenAll`, e o cálculo dos hashes foi otimizado com `Parallel.For`.

Durante a execução, foi possível observar melhor aproveitamento dos recursos da máquina e maior organização do fluxo do programa.

Tempo total observado na execução: coloque aqui o tempo que apareceu no console.

---

## 🌐 Repositório original

[https://github.com/3ES-CSharp/AsyncLab](https://github.com/3ES-CSharp/AsyncLab)

---

## 📦 Repositório da entrega

[https://github.com/VicxMouraM/CP-1-HandsOn-06](https://github.com/VicxMouraM/CP-1-HandsOn-06)

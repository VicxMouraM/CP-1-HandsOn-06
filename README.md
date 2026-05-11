# ⚡ CP-1 HandsOn 06 - AsyncLab

## 🧪 Laboratório Async

### 🎯 Objetivo

Analisar o programa e tornar a sua execução assíncrona.

---

## 👥 Membros do grupo

- Victoria Moura - RM 555474

---

## 🛠️ Descrição das modificações realizadas

O programa original foi analisado para identificar pontos que poderiam ser transformados em chamadas assíncronas e trechos que poderiam ser executados de forma paralela.

As principais modificações realizadas foram:

- Uso de `async` e `await` no fluxo principal do programa;
- Download do arquivo CSV de municípios com `HttpClient.GetStringAsync`;
- Escrita do arquivo temporário com `File.WriteAllTextAsync`;
- Leitura do CSV com `File.ReadAllLinesAsync`;
- Processamento das UFs de forma concorrente utilizando `Task.WhenAll`;
- Cálculo dos hashes dos municípios em paralelo utilizando `Parallel.For`;
- Geração dos arquivos `.csv` com `File.WriteAllLinesAsync`;
- Geração dos arquivos `.json` com `File.WriteAllTextAsync`;
- Medição do tempo de execução com `Stopwatch`;
- Exibição do tempo individual por UF e do tempo total da execução.

---

## 📊 Impactos observados no tempo de execução

Com as alterações realizadas, o programa passou a executar operações de download, leitura e escrita de arquivos de forma assíncrona, evitando bloqueios desnecessários durante a execução.

Além disso, o uso de `Task.WhenAll` permitiu que as UFs fossem processadas de forma concorrente, enquanto o `Parallel.For` acelerou o cálculo dos hashes dos municípios.

Na execução realizada, foram geradas 27 UFs.

Tempo total observado:

```txt
0m 08s 270ms

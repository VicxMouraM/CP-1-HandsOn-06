# ⚡ CP-1 HandsOn 06 - AsyncLab

## 🧪 Laboratório Async

### 🎯 Objetivo

Analisar o programa original e tornar sua execução assíncrona, identificando os pontos que poderiam ser otimizados e observando o impacto no tempo de execução.

---

## 👥 Membros do grupo

- Victoria Moura - RM 555474

---

## 🛠️ Modificações realizadas

O projeto original foi analisado para identificar quais partes poderiam ser transformadas em chamadas assíncronas e quais trechos poderiam ser executados de forma paralela.

As principais modificações realizadas foram:

- Transformação do fluxo principal para execução assíncrona com `async` e `await`;
- Download do arquivo CSV utilizando `HttpClient.GetStringAsync`;
- Escrita do arquivo temporário com `File.WriteAllTextAsync`;
- Leitura do CSV com `File.ReadAllLinesAsync`;
- Geração dos arquivos de saída com `File.WriteAllLinesAsync` e `File.WriteAllTextAsync`;
- Processamento das UFs em tarefas assíncronas utilizando `Task.WhenAll`;
- Cálculo dos hashes dos municípios de cada UF utilizando `Parallel.For`;
- Organização dos municípios por UF antes da geração dos arquivos;
- Geração de arquivos `.csv` e `.json` separados por UF;
- Medição do tempo total de execução e do tempo individual de cada UF com `Stopwatch`.

---

## 📊 Impactos observados no tempo de execução

Com a implementação assíncrona, as operações de entrada e saída deixaram de bloquear a execução principal do programa.

O uso de `Task.WhenAll` permitiu que a geração dos arquivos por UF fosse executada de forma concorrente, enquanto o `Parallel.For` ajudou a acelerar o cálculo dos hashes, que é uma operação mais pesada por utilizar PBKDF2.

Na execução do programa, foi possível observar:

- Melhor aproveitamento dos recursos da máquina;
- Redução de bloqueios durante download, leitura e escrita de arquivos;
- Execução paralela no processamento dos municípios;
- Exibição do tempo individual de cada UF processada;
- Exibição do tempo total ao final da execução.

# ⚡ CP-1 HandsOn 06 - AsyncLab

## 🧪 Laboratório Async

### 🎯 Objetivo

Analisar o programa original e tornar sua execução assíncrona, identificando os pontos que poderiam ser otimizados e observando o impacto no tempo de execução.

---

## 👥 Membros do grupo

- Victoria Moura - RM 555474

---

## 🛠️ Modificações realizadas

O projeto original foi analisado para identificar operações que poderiam ser executadas de forma assíncrona.

As principais modificações realizadas foram:

- Transformação do fluxo principal em execução assíncrona com `async` e `await`;
- Utilização de `Task` para permitir melhor organização das operações;
- Ajuste de métodos que realizavam processamento pesado para execução assíncrona;
- Separação de responsabilidades para facilitar a leitura e manutenção do código;
- Comparação do tempo de execução antes e depois das alterações;
- Manutenção da lógica original do programa, apenas otimizando a forma de execução.

---

## 📊 Impactos observados no tempo de execução

Após a implementação assíncrona, foi possível observar melhora na organização do fluxo e redução no tempo total de execução em operações que podiam ser executadas de forma paralela ou não bloqueante.

A execução assíncrona permitiu que o programa aproveitasse melhor os recursos disponíveis, evitando bloqueios desnecessários durante o processamento.

---

## 🌐 Repositório original

[https://github.com/3ES-CSharp/AsyncLab](https://github.com/3ES-CSharp/AsyncLab)

---

## 📦 Repositório da entrega

[https://github.com/VicxMouraM/CP-1-HandsOn-06](https://github.com/VicxMouraM/CP-1-HandsOn-06)
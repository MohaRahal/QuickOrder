# 📦 QuickOrder

**QuickOrder** é um aplicativo de gerenciamento de pedidos desenvolvido para **pequenos comércios**, oferecendo uma maneira prática de criar, gerenciar e visualizar pedidos.  

Com foco na **simplicidade e eficiência**, o app facilita o **controle de estoque**, geração de **relatórios em PDF** e exibição de **gráficos interativos** para análise de vendas.

---

## 🚀 Tecnologias Utilizadas

- ⚙️ **Blazor** – Framework para interfaces de usuário interativas.
- 📱 **.NET MAUI** – Desenvolvimento de apps mobile/desktop com C#.
- 💻 **C#** – Lógica de negócio e backend.
- 🗃️ **SQLite** – Banco de dados local leve.
- 🧾 **QuestPDF** – Geração de relatórios em PDF.
- 📊 **Chart.js** – Gráficos dinâmicos para relatórios visuais.
- 🎨 **Blazorise** – Componentes UI com base no Bootstrap.

---

## ✨ Funcionalidades

### ✅ Cadastro e Visualização de Pedidos
- Adição de Clientes por meio do CNPJ , para evitar dados ambíguos no banco.
- Adição de novos pedidos com informações detalhadas.
- Suporte a múltiplos itens por pedido.
- Lista de pedidos com dados como: cliente, data, valor total e quantidade.

### 📊 Relatórios de Pedidos

- Filtro por **intervalo de datas**.
- Geração de **gráficos dinâmicos** com:
  - Barras 📊  
  - Pizza 🍕  
  - Linhas 📈  
- Visualização da quantidade e valores por cliente.

### 📄 Geração de PDFs

- Relatórios detalhados em PDF com **QuestPDF**.
- Exportação de pedidos e vendas para consulta posterior.

### 📦 Controle de Estoque

- Controle de produtos disponíveis.
- Atualização automática do estoque ao realizar uma venda.
- Visualização e edição da quantidade de produtos.

---

## 🛠️ Como Funciona

### 🧾 Página de Pedidos

- Cadastro de pedidos com múltiplos itens.
- Opções de **edição** e **exclusão** de registros.

### 📈 Página de Relatórios

- Filtro por datas para analisar os pedidos.
- Exibição dos dados em **gráficos interativos** com base nos filtros.

### 🧾 Geração de PDF

- Exportação de relatórios detalhados com **dados filtrados**.
- Opção de **download direto** para o dispositivo.

### 💽 Banco de Dados

- Armazenamento local usando **SQLite**.
- Persistência de dados entre sessões.

---

## 📋 Requisitos

### 1. Instalar o .NET SDK  
Desenvolvido com **.NET 6 ou 7**.  
🔗 [Baixar SDK .NET](https://dotnet.microsoft.com/en-us/download)

### 2. Instalar o SQLite (opcional)  
A biblioteca do SQLite será instalada automaticamente.  
Caso deseje trabalhar diretamente com o banco local:  
🔗 [Baixar SQLite](https://sqlite.org/download.html)

---

## 📦 Instalação e Execução

### 📁 Clonar o repositório
```bash
git clone https://github.com/MohaRahal/QuickOrder.git
cd QuickOrder
```
###📚 Instalação de Dependências
🔹Blazorise
```bash
dotnet add package Blazorise.Bootstrap --version 0.9.0-alpha1
dotnet add package Blazorise.Icons.FontAwesome --version 0.9.0-alpha1
```
🔹QuesPDF
```bash
dotnet add package QuestPDF --version 2022.2.0
```
###🤝 Contribuindo
Contribuições são sempre bem-vindas!
Sinta-se livre para abrir issues, forks ou enviar pull requests 🚀

###📬 Contato
✉️ Email: moharahal30@gmail.com

💼 GitHub: @MohaRahal

📲 LinkedIn: https://www.linkedin.com/in/mohamed-rahal-181577274





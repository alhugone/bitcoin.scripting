# BTC Bitcion scripting

Goals of the project:

- to create a library that parses BTC Bitcoin script and compiles it to binary
  - Parser
  - Compiler
- UnitTest framework for BTC Bitcoin scripts
- personal:
  - In-depth knowledge about Bitcoin
     - deep understanding of [Bitcoin core code](https://github.com/bitcoin/bitcoin)
     - deep understanding of Bitcoin Smart Contracts and 'script language'

Some todos:
  - get rid of the static mnemonic definition
  - write tests that compare what this compiler produces to what Bitcoin core does
  - optimizations?
    - right now some part are just based on *Naive algorithms* - is it worth to optimize it

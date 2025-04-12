import { useState } from 'react'
import { Navbar } from '../components/Navbar'
import { api } from '../helpers/api'

function Cadastro() {
  const [warning, setWarning] = useState(null)
  const [loading, setLoading] = useState(false)

  const handleSubmit = async event => {
    event.preventDefault()
    setWarning(null)

    const email = document.getElementById('emailInput').value
    const senha = document.getElementById('senhaInput').value
    const nome = document.getElementById('nomeInput').value
    const telefone = document.getElementById('telefoneInput').value
    const documento = document.getElementById('documentoInput').value

    setLoading(true)
    await api
      .post('/Usuarios', {
        email: email,
        senha: senha,
        eAdmin: true,
        nome: nome,
        telefone: telefone,
        documento: documento,
      })
      .then(res => {
        setLoading(false)
        setWarning(`Usuário criado, ID: ${res.data.id}`)
      })
      .catch(err => {
        setLoading(false)
        console.log(err)
        setWarning(
          `Erro "${err.message}", consulte o console para mais informações`,
        )
      })
  }

  return (
    <div className='font-inter h-screen w-screen bg-[#efefef] antialiased'>
      <Navbar />
      <div className='flex h-full w-full items-center justify-center'>
        <div className='relative w-full max-w-xs'>
          <div className='w-full overflow-hidden rounded-xl shadow-2xl shadow-neutral-300'>
            <h1 className='w-full bg-[#3b5253] py-5 text-center tracking-wider text-white'>
              Criar Usuário
            </h1>
            <form
              id='cadastroForm'
              className='flex w-full flex-col items-center gap-3 px-2.5 py-2.5'
              onSubmit={event => handleSubmit(event)}
            >
              <Input
                id={'emailInput'}
                placeholder={'Email'}
                type={'email'}
                required
              />
              <Input
                id={'senhaInput'}
                placeholder={'Senha'}
                type={'password'}
                required
              />
              <Input
                id={'nomeInput'}
                placeholder={'Nome'}
                type={'text'}
                required
              />
              <Input
                id={'telefoneInput'}
                placeholder={'Telefone'}
                type={'text'}
                required
              />
              <Input
                id={'documentoInput'}
                placeholder={'Documento'}
                type={'text'}
                required
              />
              <input
                type='submit'
                className='cursor-pointer rounded-sm bg-[#5e8a8c] px-6 py-2 text-white transition-all hover:scale-105 active:scale-95'
                value={'Confirmar'}
              />
            </form>
          </div>
          <p className='absolute -bottom-36 h-32 w-full text-center text-lg text-[#656565]'>
            {loading && 'Carregando...'}
            {warning && warning}
          </p>
        </div>
      </div>
    </div>
  )
}

function Input({ id, placeholder, type, required }) {
  return (
    <input
      id={id}
      placeholder={placeholder}
      type={type}
      required={required}
      className='w-full rounded-lg bg-[#ffffff] px-2 py-2.5 text-[#4f4f4f] outline-0 transition-all placeholder:font-bold placeholder:text-[#b1b1b1] placeholder:italic hover:scale-105 focus:scale-105'
    />
  )
}

export { Cadastro }

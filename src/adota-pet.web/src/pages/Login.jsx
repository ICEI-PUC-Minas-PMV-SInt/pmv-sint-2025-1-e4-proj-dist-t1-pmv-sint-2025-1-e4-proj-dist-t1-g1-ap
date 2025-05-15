import { useState } from 'react'
import { Navbar } from '../components/Navbar'
import { api } from '../helpers/api'

function Login() {
  const [warning, setWarning] = useState(null)
  const [loading, setLoading] = useState(false)

  const handleSubmit = async event => {
    event.preventDefault()
    setWarning(null)

    const email = document.getElementById('emailInput').value
    const senha = document.getElementById('senhaInput').value

    setLoading(true)
    try {
      const res = await api.post('/login', {
        email: email,
        senha: senha,
      })

      setWarning(
        `Login realizado com sucesso! Bem-vindo(a) ${res.data.nome || ''}`,
      )
      // Redirecionar após login bem-sucedido, se necessário
      // window.location.href = '/dashboard'
    } catch (err) {
      console.log(err)
      setWarning(
        `Erro: "${err.message}". Verifique o console para mais detalhes.`,
      )
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className='font-inter h-screen w-screen bg-[#efefef] antialiased'>
      <Navbar />
      <div className='flex h-full w-full items-center justify-center'>
        <div className='relative w-full max-w-sm px-4'>
          <div className='w-full overflow-hidden rounded-xl shadow-2xl shadow-neutral-300'>
            <h1 className='w-full bg-[#3b5253] py-5 text-center tracking-wider text-white'>
              Login
            </h1>
            <form
              id='loginForm'
              className='flex w-full flex-col items-center gap-4 px-5 py-12'
              onSubmit={handleSubmit}
            >
              <Input
                id='emailInput'
                placeholder='Email'
                type='email'
                required
              />
              <Input
                id='senhaInput'
                placeholder='Senha'
                type='password'
                required
              />
              <div className='flex w-full gap-3'>
                <input
                  type='submit'
                  className='flex-1 cursor-pointer rounded-md bg-[#5e8a8c] px-6 py-2 text-white transition-all hover:scale-105 active:scale-95'
                  value='Entrar'
                />
                <button
                  type='button'
                  onClick={() => (window.location.href = '/.')}
                  className='flex-1 cursor-pointer rounded-md bg-[#cccccc] px-6 py-2 text-[#333] transition-all hover:scale-105 active:scale-95'
                >
                  Criar uma conta
                </button>
              </div>
            </form>
          </div>
          <p className='mt-4 text-center text-lg text-[#656565]'>
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
      className='w-full rounded-lg bg-white px-3 py-3 text-[#4f4f4f] outline-0 transition-all placeholder:font-bold placeholder:text-[#b1b1b1] placeholder:italic hover:scale-105 focus:scale-105'
    />
  )
}

export { Login }

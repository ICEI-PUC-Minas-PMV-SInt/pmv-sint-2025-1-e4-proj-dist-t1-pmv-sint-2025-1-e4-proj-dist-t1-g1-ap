import { useState } from 'react'
import { Navbar } from '../components/Navbar'
import { api } from '../helpers/api'
import { useNavigate } from 'react-router-dom'

function Login() {
  const [warning, setWarning] = useState(null)
  const [loading, setLoading] = useState(false)
  const navigate = useNavigate()

  const getUserIdByToken = token => {
    try {
      const base64Url = token.split('.')[1]
      const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/')
      const jsonPayload = decodeURIComponent(
        atob(base64)
          .split('')
          .map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
          .join(''),
      )
      const parsedJwt = JSON.parse(jsonPayload)
      return parsedJwt.nameid
    } catch (e) {
      console.error('Token invalido', e)
      return null
    }
  }

  const handleSubmit = async event => {
    event.preventDefault()
    setWarning(null)

    const documento = document.getElementById('documentoInput').value
    const senha = document.getElementById('senhaInput').value

    setLoading(true)
    await api
      .post('Usuarios/authenticate', {
        documento,
        senha,
      })
      .then(res => {
        localStorage.setItem('token', res.data.jwtToken)
        localStorage.setItem('userId', getUserIdByToken(res.data.jwtToken))
        setLoading(false)
        navigate('/usuario')
      })
      .catch(err => {
        console.error(err)
        if (err.status == 401) {
          setWarning(`Login não autorizado, verifique as credenciais`)
        } else {
          setWarning(
            `Erro: "${err.message}". Verifique o console para mais detalhes.`,
          )
        }
        setLoading(false)
      })
  }

  return (
    <div className='font-inter h-screen w-screen bg-[#efefef] antialiased'>
      <Navbar />
      <div className='flex h-full w-full items-center justify-center'>
        <div className='relative w-full max-w-sm px-1'>
          <div className='w-full overflow-hidden rounded-xl shadow-2xl shadow-neutral-300'>
            <h1 className='w-full bg-[#3b5253] py-5 text-center tracking-wider text-white'>
              Login
            </h1>
            <form
              id='loginForm'
              className='flex w-full flex-col items-center gap-3 px-2.5 py-8.5'
              onSubmit={handleSubmit}
            >
              <Input
                id='documentoInput'
                placeholder='Documento'
                type='text'
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
                  className='flex-1 cursor-pointer rounded-sm bg-[#5e8a8c] px-1 py-2 text-white transition-all hover:scale-105 active:scale-95'
                  value='Entrar'
                />
                <button
                  type='button'
                  onClick={() => navigate('/cadastro')}
                  className='flex-1 cursor-pointer rounded-md bg-[#cccccc] px-1 py-2 text-[#333] transition-all hover:scale-105 active:scale-95'
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

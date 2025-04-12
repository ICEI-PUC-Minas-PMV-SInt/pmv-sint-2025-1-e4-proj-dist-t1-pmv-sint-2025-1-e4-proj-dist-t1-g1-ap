import { useState } from 'react'
import { Navbar } from '../components/Navbar'
import { api } from '../helpers/api'

function Criar_Anuncio() {
  const [warning, setWarning] = useState(null)
  const [loading, setLoading] = useState(false)

  const handleSubmit = async event => {
    event.preventDefault()
    setWarning(null)

    const titulo = document.getElementById('tituloInput').value
    const descricao = document.getElementById('descricaoInput').value
    //const categoria = document.getElementById('categoriaSelect').value
    const raca = document.getElementById('racaInput').value
    const idade = document.getElementById('idadeInput').value

    setLoading(true)
    await api
      .post('/Anuncios', {
        titulo: titulo,
        idadeAnimal: idade,
        categoriaAnimal: 0, // categoriaAnimal: categoria,
        racaAnimal: raca,
        descricao: descricao,
        imagemCapa: 'string',
        usuarioId: 3,
      })
      .then(res => {
        setLoading(false)
        setWarning(`Anúncio criado com sucesso! ID: ${res.data.id}`)
      })
      .catch(err => {
        setLoading(false)
        console.error(err)
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
              Criar Anúncio
            </h1>
            <form
              id='criar_anuncio_form'
              className='flex w-full flex-col items-center gap-3 px-2.5 py-2.5'
              onSubmit={handleSubmit}
            >
              <Input
                id='tituloInput'
                placeholder='Título do anúncio'
                type='text'
                required
              />
              <Input
                id='descricaoInput'
                placeholder='Descrição'
                type='text'
                required
              />
              {/*<SelectCategoria id='categoriaSelect' required />*/}
              <Input id='racaInput' placeholder='Raça' type='text' required />
              <Input
                id='idadeInput'
                placeholder='Idade'
                type='number'
                required
              />

              {/* Botões */}
              <div className='flex w-full gap-2'>
                <button
                  type='button'
                  onClick={() => (window.location.href = '/')}
                  className='flex-1 cursor-pointer rounded-sm bg-[#cccccc] px-6 py-2 text-[#333] transition-all hover:scale-105 active:scale-95'
                >
                  Voltar
                </button>
                <input
                  type='submit'
                  className='flex-1 cursor-pointer rounded-sm bg-[#5e8a8c] px-6 py-2 text-white transition-all hover:scale-105 active:scale-95'
                  value='Criar'
                />
              </div>
            </form>
          </div>
          <p className='absolute -bottom-36 h-32 w-full text-center text-lg text-[#656565]'>
            {loading && 'Publicando...'}
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
      min={0}
      required={required}
      className='w-full rounded-lg bg-[#ffffff] px-2 py-2.5 text-[#4f4f4f] outline-0 transition-all placeholder:font-bold placeholder:text-[#b1b1b1] placeholder:italic hover:scale-105 focus:scale-105'
    />
  )
}

/*function SelectCategoria({ id, required }) {
  return (
    <select
      id={id}
      required={required}
      className='w-full rounded-lg bg-[#ffffff] px-2 py-2.5 font-bold text-[#4f4f4f] outline-0 transition-all hover:scale-105 focus:scale-105'
      defaultValue=''
    >
      <option value='' disabled>
        Selecione a categoria
      </option>
      <option value='Canino'>Canino</option>
      <option value='Felino'>Felino</option>
      <option value='Ave'>Ave</option>
    </select>
  )
}*/

export { Criar_Anuncio }

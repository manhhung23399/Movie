package com.manhhung.movie.base

interface BindDataAdapter<T> {
    fun setData(data: List<T>?)
}

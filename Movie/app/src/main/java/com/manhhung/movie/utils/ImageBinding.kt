package com.manhhung.movie.utils

import android.view.View
import android.widget.ImageView
import androidx.databinding.BindingAdapter
import com.bumptech.glide.Glide

@BindingAdapter("app:loadImage")
fun ImageView.loadImage(uri: String?) {
    Glide.with(context)
        .load(uri)
        .into(this)
}

@BindingAdapter("app:visibility")
fun View.checkImage(value: Boolean) {
    visibility = if (value) View.VISIBLE else View.GONE
}

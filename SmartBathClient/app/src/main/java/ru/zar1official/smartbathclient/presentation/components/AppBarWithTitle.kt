package ru.zar1official.smartbathclient.presentation.components

import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.material.Text
import androidx.compose.material.TopAppBar
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.text.style.TextAlign

@Composable
fun AppBarWithTitle(
    modifier: Modifier = Modifier,
    title: String,
    textAlign: TextAlign,
) {
    TopAppBar(
        modifier = modifier,
        title = {
            Text(text = title, modifier = Modifier.fillMaxWidth(), textAlign = textAlign)
        }
    )
}
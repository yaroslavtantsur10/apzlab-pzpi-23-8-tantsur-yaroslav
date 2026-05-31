package com.comfortspace.ui

import android.os.Bundle
import android.view.View
import android.widget.*
import androidx.fragment.app.Fragment
import androidx.fragment.app.viewModels
import androidx.lifecycle.ViewModel
import androidx.lifecycle.ViewModelProvider
import com.comfortspace.R
import com.comfortspace.data.remote.RetrofitClient
import com.comfortspace.data.repository.NotificationRepository
import com.comfortspace.viewmodel.NotificationsViewModel

class NotificationsFragment : Fragment(R.layout.fragment_notifications) {

    private val viewModel: NotificationsViewModel by viewModels {
        object : ViewModelProvider.Factory {
            override fun <T : ViewModel> create(modelClass: Class<T>): T {
                val api = RetrofitClient.create(requireContext())
                @Suppress("UNCHECKED_CAST")
                return NotificationsViewModel(NotificationRepository(api)) as T
            }
        }
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)

        val listView = view.findViewById<ListView>(R.id.listNotifications)
        val progressBar = view.findViewById<ProgressBar>(R.id.progressBar)
        val tvEmpty = view.findViewById<TextView>(R.id.tvEmpty)

        viewModel.loadNotifications()

        viewModel.isLoading.observe(viewLifecycleOwner) { loading ->
            progressBar.visibility = if (loading) View.VISIBLE else View.GONE
        }

        viewModel.notifications.observe(viewLifecycleOwner) { result ->
            result.onSuccess { notifications ->
                if (notifications.isEmpty()) {
                    tvEmpty.visibility = View.VISIBLE
                    listView.visibility = View.GONE
                } else {
                    tvEmpty.visibility = View.GONE
                    listView.visibility = View.VISIBLE
                    val items = notifications.map { "${it.title}\n${it.message}" }
                    listView.adapter = ArrayAdapter(
                        requireContext(),
                        android.R.layout.simple_list_item_1,
                        items
                    )
                    listView.setOnItemClickListener { _, _, position, _ ->
                        viewModel.markAsRead(notifications[position].notificationId)
                    }
                }
            }
            result.onFailure {
                Toast.makeText(requireContext(), it.message, Toast.LENGTH_SHORT).show()
            }
        }
    }
}


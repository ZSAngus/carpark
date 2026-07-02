using System;

namespace CarPark2018.Device.CashierBusiness;

public class ComQueue
{
	private const int _MinimumGrow = 4;

	private byte[] _array;

	private int _head;

	private int _tail;

	private int _size;

	private int _growFactor;

	public ComQueue()
	{
	}

	public ComQueue(int capacity)
	{
	}

	public ComQueue(int capacity, float growFactor)
	{
		if (capacity < 0)
		{
			throw new ArgumentOutOfRangeException("capacity", "ArgumentOutOfRange_NeedNonNegNum");
		}
		if ((double)growFactor < 1.0 || (double)growFactor > 10.0)
		{
			throw new ArgumentOutOfRangeException("growFactor", "ArgumentOutOfRange_QueueGrowFactor");
		}
		_array = new byte[capacity];
		_head = 0;
		_tail = 0;
		_size = 0;
		_growFactor = (int)(growFactor * 100f);
	}

	public void Clear()
	{
		if (_head < _tail)
		{
			Array.Clear(_array, _head, _size);
		}
		else
		{
			Array.Clear(_array, _head, _array.Length - _head);
			Array.Clear(_array, 0, _tail);
		}
		_head = 0;
		_tail = 0;
		_size = 0;
	}

	public void Enqueue(byte inByte)
	{
		if (_size == _array.Length)
		{
			int num = (int)((long)_array.Length * (long)_growFactor / 100);
			if (num < _array.Length + 4)
			{
				num = _array.Length + 4;
			}
			SetCapacity(num);
		}
		_array[_tail] = inByte;
		_tail = (_tail + 1) % _array.Length;
		_size++;
	}

	private byte ByteDequeue()
	{
		if (_size == 0)
		{
			throw new InvalidOperationException("InvalidOperation_EmptyQueue");
		}
		byte result = _array[_head];
		_head = (_head + 1) % _array.Length;
		_size--;
		return result;
	}

	public TicketReaderResponse Dequeue()
	{
		TicketReaderResponse ticketReaderResponse = Peek();
		if (ticketReaderResponse != null)
		{
			_head = (_head + ticketReaderResponse.Length) % _array.Length;
			_size -= ticketReaderResponse.Length;
		}
		return ticketReaderResponse;
	}

	private byte BytePeek()
	{
		if (_size == 0)
		{
			throw new InvalidOperationException("InvalidOperation_EmptyQueue");
		}
		return _array[_head];
	}

	private int Find(byte byt)
	{
		int result = -1;
		for (int i = 0; i < _array.Length; i++)
		{
			if (_array[i] == byt)
			{
				return i;
			}
		}
		return result;
	}

	public TicketReaderResponse Peek()
	{
		while (_size > 0 && BytePeek() != 2)
		{
			ByteDequeue();
		}
		if (_size < 2)
		{
			return null;
		}
		int num = Find(13);
		int num2 = num - _head + 1;
		if (num <= 0 || num2 <= 0)
		{
			return null;
		}
		if (num2 > 50)
		{
			ByteDequeue();
			return Peek();
		}
		if (_size < num2)
		{
			return null;
		}
		byte[] array = new byte[num2];
		for (int i = 0; i < num2; i++)
		{
			array[i] = GetElement(i);
		}
		return TicketReaderResponse.FromBytes(array);
	}

	private byte GetElement(int i)
	{
		return _array[(_head + i) % _array.Length];
	}

	private void SetCapacity(int capacity)
	{
		byte[] array = new byte[capacity];
		if (_size > 0)
		{
			if (_head < _tail)
			{
				Array.Copy(_array, _head, array, 0, _size);
			}
			else
			{
				Array.Copy(_array, _head, array, 0, _array.Length - _head);
				Array.Copy(_array, 0, array, _array.Length - _head, _tail);
			}
		}
		_array = array;
		_head = 0;
		_tail = ((_size != capacity) ? _size : 0);
	}

	internal TicketReaderResponse GetMessageAt(int p)
	{
		throw new Exception("TicketReaderResponse GetMessageAt出错");
	}
}

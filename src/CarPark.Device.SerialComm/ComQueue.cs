using System;
using CarPark.Device.SerialComm;

public class ComQueue
{
	private const int _MinimumGrow = 4;

	private byte[] _array;

	private int _head;

	private int _tail;

	private int _size;

	private int _growFactor;

	public ComQueue()
		: this(32, 2f)
	{
	}

	public ComQueue(int capacity)
		: this(capacity, 2f)
	{
	}

	public ComQueue(int capacity, float growFactor)
	{
		Class2.WKJkUh2zLspup();
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

	public ACEResponse Dequeue()
	{
		ACEResponse aCEResponse = Peek();
		if (aCEResponse != null)
		{
			_head = (_head + aCEResponse.LEN_Short + 4) % _array.Length;
			_size = _size - aCEResponse.LEN_Short - 4;
		}
		return aCEResponse;
	}

	private byte BytePeek()
	{
		if (_size == 0)
		{
			throw new InvalidOperationException("InvalidOperation_EmptyQueue");
		}
		return _array[_head];
	}

	public ACEResponse Peek()
	{
		while (_size > 0 && BytePeek() != 2)
		{
			ByteDequeue();
		}
		if (_size < 8)
		{
			return null;
		}
		int num = GetElement(4) + 1;
		if (num > 128)
		{
			ByteDequeue();
			return Peek();
		}
		if (_size < num)
		{
			return null;
		}
		if (GetElement(num - 2) != 3)
		{
			ByteDequeue();
			return Peek();
		}
		byte b = 0;
		for (int i = 1; i < num - 1; i++)
		{
			b ^= GetElement(i);
		}
		if (b != GetElement(num - 1))
		{
			ByteDequeue();
			return Peek();
		}
		byte[] array = new byte[num];
		for (int j = 0; j < num; j++)
		{
			array[j] = GetElement(j);
		}
		return ACEResponse.FromBytes(array);
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
}
